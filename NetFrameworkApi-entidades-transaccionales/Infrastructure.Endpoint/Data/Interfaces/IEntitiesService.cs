using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Builders;

namespace Infrastructure.Endpoint.Data.Interfaces
{
    public interface IEntitiesService
    {
        SqlEntitySettings GetSettings<T>() where T : BaseEntity;
    }
}