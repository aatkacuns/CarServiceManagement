using System.Data;
using System.Threading.Tasks;

namespace CarServiceManagement.MySQL
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}