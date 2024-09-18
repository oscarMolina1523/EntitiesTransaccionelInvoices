using Application.Endpoint.Commands;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Endpoint.Handlers.Commands
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Dish>
    {
        private readonly IDishesRepository dishesRepository;
        public CreateDishCommandHandler(IDishesRepository dishesRepository)
        {
            this.dishesRepository = dishesRepository;
        }

        public async Task<Dish> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            Dish dish = new Dish
            {
                Id = Guid.NewGuid(),
                Name = request.DishDto.Name,
                Price = request.DishDto.Price,
                Description = request.DishDto.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = request.UserId
            };
            await dishesRepository.CreateAsync(dish);

            return dish;
        }
    }
}
