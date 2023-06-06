using Employment.API.DTOs;
using Employment.API.Entities;
using System.Linq.Expressions;

namespace Employment.API.Extensions
{
    public static class Extensions
    {
        public static ClientJob AsDTO(this JobPosition jobPosition)
        {
            return new ClientJob(jobPosition.Id,  jobPosition.ClientId, jobPosition.Title, jobPosition.Description, jobPosition.Requisites);
        }
        public static JobPosition FomDTO(this ClientJob clientJob)
        {
            return new JobPosition()
            {
                ClientId = clientJob.ClientId,
                Title = clientJob.Title,
                Description = clientJob.Description,
                Id = clientJob.Id,
                Requisites = clientJob.Requisites,
            };
        }
    }
}
