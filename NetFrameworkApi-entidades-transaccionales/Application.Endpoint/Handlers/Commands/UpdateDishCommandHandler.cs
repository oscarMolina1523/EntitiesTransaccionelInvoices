using Application.Endpoint.Commands;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Exceptions;
using Domain.Endpoint.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Endpoint.Handlers.Commands
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Dish>
    {
        private readonly IDishesRepository dishesRepository;
        public UpdateDishCommandHandler(IDishesRepository dishesRepository)
        {
            this.dishesRepository = dishesRepository;
        }

        public async Task<Dish> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await dishesRepository.GetByIdAsync(request.Id);

            if (dish is null) throw new EntityNotFoundException(request.Id, nameof(Dish));

            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.UpdatedAt = DateTime.Now;
            dish.UpdatedBy = request.UserId;
            dish.Price = request.Price;
            await dishesRepository.UpdateAsync(dish);

            return dish;
        }
    }
}
