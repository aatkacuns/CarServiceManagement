using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CarServiceManagement.MySQL
{
    public interface IMySqlRepository
    {
        Task<T> SelectSingleItemAsync<T>(string query);
        
        Task<T> SelectSingleItemAsync<T>(string query, object param);

        Task<IReadOnlyList<T>> SelectItemsAsync<T>(string query);
        
        Task<IReadOnlyList<T>> SelectItemsAsync<T>(string query, object param);

        Task<IReadOnlyList<object>> SelectMultipleItemsAsync(string query,
            params Func<SqlMapper.GridReader, object>[] funcs);
        
        Task<IReadOnlyList<object>> SelectMultipleItemsAsync(string query, object param,
            params Func<SqlMapper.GridReader, object>[] funcs);

        Task<int> ExecuteAsync(string query);
        
        Task<int> ExecuteAsync(string query, object param);

        Task ExecuteInTransactionAsync(IEnumerable<(string query, object param)> queriesToExecute);
    }
}