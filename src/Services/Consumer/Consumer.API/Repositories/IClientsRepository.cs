using Consumer.API.Entities;

namespace Consumer.API.Repositories
{
    public interface IClientsRepository
    {
        Task CreateClient(Client client);
        Task DeleteClient(Guid id);
        Task<IReadOnlyCollection<Client>> GetAllClients();
        Task<Client> GetClientById(Guid id);
        Task UpdateClient(Guid id, Client client);
    }
}