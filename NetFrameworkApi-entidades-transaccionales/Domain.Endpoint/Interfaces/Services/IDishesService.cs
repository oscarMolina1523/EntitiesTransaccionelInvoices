using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IDishesService
    {
        Task<List<Dish>> Get();
        Task<Dish> GetByIdAsync(Guid id);
        //Task<Dish> CreateAsync(CreateDishDto dishDto);
        //Task<ToDo> UpdateAsync(Guid id, UpdateToDoDto toDo);
        Task<Dish> DeleteAsync(Guid id);
    }
}
