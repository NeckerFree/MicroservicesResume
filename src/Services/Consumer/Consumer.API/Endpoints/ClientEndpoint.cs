using Consumer.API.DTOs;
using Consumer.API.Extensions;
using Consumer.API.Repositories;
using System.Runtime.CompilerServices;

namespace Consumer.API.Endpoints
{
    public static class ClientEndpoint
    {
        
        public static void MapClientEndpoint(this IEndpointRouteBuilder routes)
        {
            //IClientsRepository _clientsRepository=clientsRepository;
        
            var group = routes.MapGroup("/api/Client");

            group.MapGet("/", async Task<IEnumerable<ClientDTO>> (IClientsRepository _clientsRepository) =>
            {
                var clients= (await _clientsRepository.GetAllClients())
                .Select(cl=>cl.AsDto());
                return clients;
            })
            .WithName("GetAllClients");

            group.MapGet("/{id}", async Task<ClientDTO> (Guid id, IClientsRepository _clientsRepository) =>
            {
              var  client=await _clientsRepository.GetClientById(id);
                return client.AsDto();
            })
            .WithName("GetClientById");

                      group.MapPost("/", async (ClientDTO clientDTO, IClientsRepository _clientsRepository) =>
            {
                await _clientsRepository.CreateClient( clientDTO.FromDto());
            })
            .WithName("CreateClient");

            group.MapPut("/{id}", async Task (Guid id, ClientDTO clientDTO, IClientsRepository _clientsRepository) =>
            {
                await _clientsRepository.UpdateClient(id, clientDTO.FromDto());
            })
            .WithName("UpdateClient");

            group.MapDelete("/{id}", async Task (Guid id, IClientsRepository _clientsRepository) =>
            {
                 await _clientsRepository.DeleteClient(id);
            })
            .WithName("DeleteClient");
        }
    }
}
