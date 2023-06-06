using Consumer.API.DTOs;
using Consumer.API.Entities;
using Consumer.API.Extensions;
using Parser.Common.Repositories;

namespace Consumer.API.Endpoints
{
    public static class ClientEndpoint
    {

        public static void MapClientEndpoint(this IEndpointRouteBuilder routes)
        {
           var group = routes.MapGroup("/api/Client");

            group.MapGet("/", async (IRepository<Client> _clientsRepository) =>
            {
                var clients = (await _clientsRepository.GetAllAsync())
                .Select(cl => cl.AsDto());
                return Results.Ok(clients);
            })
            .WithName("GetAllClients");

            group.MapGet("/{id}", async (Guid id, IRepository<Client> _clientsRepository) =>
            {
                Client client = await _clientsRepository.GetByIdAsync(id);
                return (client != null)
                ? Results.Ok(client.AsDto())
                : Results.NotFound();
            })
            .WithName("GetClientById");


            group.MapPost("/", async (ClientDTO clientDTO, IRepository<Client> _clientsRepository) =>
            {
                await _clientsRepository.CreateAsync(clientDTO.FromDto());
            })
            .WithName("CreateClient");

            group.MapPut("/{id}", async (Guid id, ClientDTO clientDTO, IRepository<Client> _clientsRepository) =>
            {
                Client client = await _clientsRepository.GetByIdAsync(id);
                if (client == null) return Results.NotFound();
                else
                {
                    await _clientsRepository.UpdateAsync(id, clientDTO.FromDto());
                   return Results.NoContent();
                }
             })
            .WithName("UpdateClient");

            group.MapDelete("/{id}", async (Guid id, IRepository<Client> _clientsRepository) =>
            {
                Client client = await _clientsRepository.GetByIdAsync(id);
                if (client == null) return Results.NotFound();
                else
                {
                    await _clientsRepository.DeleteAsync(id);
                    return Results.NoContent();
                }
            })
            .WithName("DeleteClient");
        }
    }
}
