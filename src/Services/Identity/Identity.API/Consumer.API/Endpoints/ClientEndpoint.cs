using Consumer.API.DTOs;
using Consumer.API.Extensions;
using Consumer.API.Repositories;

namespace Consumer.API.Endpoints
{
    public static class ClientEndpoint
    {
        private static readonly ClientsRepository clientsRepository = new();
        public static void MapClientEndpoint(this IEndpointRouteBuilder routes)
        {
            
            var group = routes.MapGroup("/api/Client");

            group.MapGet("/", async Task<IEnumerable<ClientDTO>> () =>
            {
                var clients= (await clientsRepository.GetAllClients())
                .Select(cl=>cl.AsDto());
                return clients;
            })
            .WithName("GetAllClients");

            group.MapGet("/{id}", async Task<ClientDTO> (Guid id) =>
            {
              var  client=await clientsRepository.GetClientById(id);
                return client.AsDto();
            })
            .WithName("GetClientById");

            group.MapPut("/{id}", async Task (Guid id, ClientDTO clientDTO) =>
            {
                await clientsRepository.UpdateClient(id,clientDTO.FromDto());
            })
            .WithName("UpdateClient");

            group.MapPost("/", async (ClientDTO clientDTO) =>
            {
                await clientsRepository.CreateClient( clientDTO.FromDto());
            })
            .WithName("CreateClient");

            group.MapDelete("/{id}", async Task (Guid id ) =>
            {
                 await clientsRepository.DeleteClient(id);
            })
            .WithName("DeleteClient");
        }
    }
}
