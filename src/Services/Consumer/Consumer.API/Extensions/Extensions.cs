using Consumer.API.DTOs;
using Consumer.API.Entities;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace Consumer.API.Extensions
{
    public static class Extensions
    {
        public static ClientDTO AsDto(this Client client)
        {
            return new ClientDTO()
            {
                Address = client.Address,
                City = client.City,
                Country = client.Country,
                Description = client.Description,
                Fax = client.Fax,
                Id = client.Id,
                Name = client.Name,
                Phone = client.Phone,
                PostalCode = client.PostalCode,
                Region = client.Region,
                Status = client.Status,
            };
        }

        public static Client FromDto(this ClientDTO clientDTO)
        {
            return new Client()
            {
                Address = clientDTO.Address,
                City = clientDTO.City,
                Country = clientDTO.Country,
                Description = clientDTO.Description,
                Fax = clientDTO.Fax,
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                Phone = clientDTO.Phone,
                PostalCode = clientDTO.PostalCode,
                Region = clientDTO.Region,
                Status = clientDTO.Status,
            };
        }
    }
}
