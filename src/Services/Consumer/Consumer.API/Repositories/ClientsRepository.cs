using Consumer.API.DTOs;
using Consumer.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace Consumer.API.Repositories
{
    public class ClientsRepository
    {
        private const string collectionName = "Clients";
        private readonly IMongoCollection<Client> dbCollection;
        private readonly FilterDefinitionBuilder<Client> filterBuilder = Builders<Client>.Filter;
        public ClientsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Clients");
            dbCollection = database.GetCollection<Client>(collectionName);
        }
        public async Task<IReadOnlyCollection<Client>> GetAllClients()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Client> GetClientById(Guid id)
        {
            FilterDefinition<Client> filter = filterBuilder.Eq(client => client.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();

        }

        public async Task CreateClient(Client client)

        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            await dbCollection.InsertOneAsync(client);
        }

        public async Task UpdateClient(Guid id, Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            FilterDefinition<Client> filter = filterBuilder.Eq(existentClient => existentClient.Id, id);
          await dbCollection.ReplaceOneAsync(filter, client);

        }


        public async Task DeleteClient(Guid id)
        {
            FilterDefinition<Client> filter = filterBuilder.Eq(client => client.Id, id);
            await dbCollection.DeleteOneAsync(filter);

        }

        
    }
}
