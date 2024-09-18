using Application.Endpoint.Queries;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Endpoint.Handlers.Queries
{
    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, List<Dish>>
    {
        private readonly IDishesRepository dishesRepository;
        public GetAllDishesQueryHandler(IDishesRepository dishesRepository)
        {
            this.dishesRepository = dishesRepository;
        }

        public async Task<List<Dish>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await dishesRepository.GetAsync();
            return dishes;
        }
    }
}
