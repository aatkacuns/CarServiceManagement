using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceManagement.Models;

namespace CarServiceManagement.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<int> UpdateClientInformation(Client entity);
        Task<int> DeleteClientByIdAsync(int id);
        Task<int> AddClientAsync(Client entity);
    }
}