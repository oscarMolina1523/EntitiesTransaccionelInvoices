using System;

namespace Domain.Endpoint.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Guid Id { get; set; }
        public EntityNotFoundException(Guid id) : base($"Entity with Id {id} Not Found!")
        {
            Id = id;
            Data.Add("id", Id);
        }

        public EntityNotFoundException(Guid id, string entityName) : base($"Entity {entityName} with Id {id} Not Found!")
        {
            Id = id;
            Data.Add("Id", Id);
        }
    }
}
