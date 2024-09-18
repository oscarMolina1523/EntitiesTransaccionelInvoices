using Domain.Endpoint.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Endpoint.Queries
{
    public class GetAllDishesQuery : IRequest<List<Dish>> { }
}
