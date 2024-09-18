using Domain.Endpoint.Entities;
using MediatR;
using System;

namespace Application.Endpoint.Commands
{
    public class UpdateDishCommand : IRequest<Dish>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }

        public UpdateDishCommand() { }

        public UpdateDishCommand(Guid id, string name, string description, decimal price, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
        }
    }
}
