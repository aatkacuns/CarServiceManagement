using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace CarServiceManagement.MySQL
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string connString =
            @"host=127.0.0.1;port=3306;uid=easycruit;pwd=kt74caFhNdrM8jwd;database=Morph;Pooling=true;MaximumPoolsize=10;MinimumPoolSize=10;AllowZeroDatetime=true;SslMode=Required;";

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            return new MySqlConnection(connString);
        }
    }
}