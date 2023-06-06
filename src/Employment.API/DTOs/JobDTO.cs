using Employment.API.Entities;
using System.Collections.Specialized;

namespace Employment.API.DTOs
{
    public record ClientJob(Guid Id, Guid ClientId, string? Title, string? Description, string? Requisites);
}
