using System.ComponentModel.DataAnnotations;

namespace Consumer.API.DTOs
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }

    }
}
