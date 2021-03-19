using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace CarServiceManagement.MySQL
{
    public class MySqlRepository : IMySqlRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MySqlRepository(IDbConnectionFactory connectionFactory)
        {
   
            _connectionFactory = connectionFactory;
          
        }
         public Task<T> SelectSingleItemAsync<T>(string query)
        {
            return SelectSingleItemAsync<T>(query, null);
        }

        public Task<T> SelectSingleItemAsync<T>(string query, object param)
        {
            ValidateQueryArgument(query);

            return ExecuteAction(connection => connection.QueryFirstOrDefaultAsync<T>(query, param));
        }

        public Task<IReadOnlyList<T>> SelectItemsAsync<T>(string query)
        {
            return SelectItemsAsync<T>(query, null);
        }

        public async Task<IReadOnlyList<T>> SelectItemsAsync<T>(string query, object param)
        {
            ValidateQueryArgument(query);

            var result = await ExecuteAction(connection => connection.QueryAsync<T>(query, param))
                .ConfigureAwait(false);
            return result?.ToList() ?? new List<T>();
        }

        public Task<IReadOnlyList<object>> SelectMultipleItemsAsync(string query,
            params Func<SqlMapper.GridReader, object>[] funcs)
        {
            return SelectMultipleItemsAsync(query, null, funcs);
        }

        public async Task<IReadOnlyList<object>> SelectMultipleItemsAsync(string query, object param,
            params Func<SqlMapper.GridReader, object>[] funcs)
        {
            ValidateQueryArgument(query);

            if (funcs == null)
            {
                throw new ArgumentNullException(nameof(funcs));
            }

            var result = await ExecuteAction(async connection =>
            {
                using (var gridReader = await connection.QueryMultipleAsync(query, param).ConfigureAwait(false))
                {
                    return funcs.Select(func => func(gridReader)).ToList();
                }
            }).ConfigureAwait(false);
            return result ?? new List<object>();
        }

        public Task<int> ExecuteAsync(string query)
        {
            return ExecuteAsync(query, null);
        }

        public Task<int> ExecuteAsync(string query, object param)
        {
            ValidateQueryArgument(query);

            return ExecuteAction(connection => connection.ExecuteAsync(query, param));
        }

        public async Task ExecuteInTransactionAsync(IEnumerable<(string query, object param)> queriesToExecute)
        {
            if (queriesToExecute == null)
            {
                throw new ArgumentNullException(nameof(queriesToExecute));
            }

            await ExecuteAction(async connection =>
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    await TryExecuteInTransaction(queriesToExecute, connection, transaction)
                        .ConfigureAwait(false);
                }

                connection.Close();
                return true;
            }).ConfigureAwait(false);
        }

        private async Task TryExecuteInTransaction(IEnumerable<(string query, object param)> queriesToExecute,
            IDbConnection connection,
            IDbTransaction transaction)
        {
            try
            {
                foreach (var (query, param) in queriesToExecute)
                {
                    await connection.ExecuteAsync(query, param, transaction).ConfigureAwait(false);
                }

                transaction.Commit();
            }
            catch (DbException e)
            {
                transaction.Rollback();
                throw;
            }
        }

        private async Task<T> ExecuteAction<T>(Func<IDbConnection, Task<T>> runAction)
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false);
                return await runAction.Invoke(connection).ConfigureAwait(false);
            }
            catch (DbException e)
            {
                throw;
            }
        }

        private static void ValidateQueryArgument(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("SQL query is invalid: " + nameof(query));
            }
        }
    }
}