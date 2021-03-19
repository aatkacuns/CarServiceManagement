using System.Collections.Generic;
using System.Threading.Tasks;
using CarServiceManagement.Exceptions;
using CarServiceManagement.Models;
using CarServiceManagement.MySQL;
using CarServiceManagement.Repositories.Interfaces;

namespace CarServiceManagement.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private IMySqlRepository _mySqlRepository;

        public ClientRepository(IMySqlRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }
        
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            const string sql = "SELECT client_id, client_name, client_middleName, client_surname, client_address, client_phoneNumber FROM clients";
            var res = await _mySqlRepository.SelectItemsAsync<Client>(sql);
            return res;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var sql = "SELECT client_id, client_name, client_middleName, client_surname, client_address, client_phoneNumber FROM clients WHERE client_id =@ClientId";
            var result = await _mySqlRepository.SelectSingleItemAsync<Client>(sql, new {ClientId = id});
            if (result == null)
            {
                throw new ValueNotFoundException("Value is null");
            }
            return result;
        }

        public async Task<int> UpdateClientInformation(Client entity)
        {
            var sql = "UPDATE clients SET client_name = @ClientName, client_middleName = @ClientMiddleName, client_surname = @ClientSurname,client_address = @ClientAddress, client_phoneNumber = @ClientPhoneNumber WHERE client_id= @ClientId";
            var result = await _mySqlRepository.ExecuteAsync(sql, entity);
            return result;
        }

        public async Task<int> DeleteClientByIdAsync(int id)
        {
            var sql = "DELETE FROM clients WHERE client_id = @ClientId";
            var result = await _mySqlRepository.ExecuteAsync(sql, new {ClientId = id});
            return result;
        }

        public async Task<int> AddClientAsync(Client entity)
        {
            var sql = @"Insert into clients (client_name, client_middleName, client_surname, client_address, client_phoneNumber) VALUES (@ClientName, @Client_MiddleName, @ClientSurname, @ClientAddress, @ClientPhoneNumber)";
            var result = await _mySqlRepository.ExecuteAsync(sql, entity);
            return result;
        }


    }
}