using Application.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using MediatR;
using System;

namespace Application.Endpoint.Commands
{
    public class CreateDishCommand : IRequest<Dish>
    {
        public readonly CreateDishDto DishDto;
        public readonly Guid UserId;

        public CreateDishCommand(CreateDishDto dishDto, Guid userId)
        {
            DishDto = dishDto;
            UserId = userId;
        }
    }
}
