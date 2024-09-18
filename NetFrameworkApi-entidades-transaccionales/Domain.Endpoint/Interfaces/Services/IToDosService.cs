using Domain.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IToDosService
    {
        Task<List<ToDo>> GetAll();
        Task<ToDo> GetByIdAsync(Guid id);
        Task<ToDo> CreateAsync(CreateToDoDto toDo);
        Task<ToDo> UpdateAsync(Guid id, UpdateToDoDto dishDto);
        Task<ToDo> DeleteAsync(Guid id);
    }
}
