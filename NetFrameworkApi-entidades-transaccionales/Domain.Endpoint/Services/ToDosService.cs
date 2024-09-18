using Domain.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class ToDosService : IToDosService
    {
        private readonly IToDosRepository toDosRepository;
        public ToDosService(IToDosRepository toDosRepository)
        {
            this.toDosRepository = toDosRepository;
        }

        public async Task<ToDo> CreateAsync(CreateToDoDto toDoDto)
        {
            ToDo toDo = new ToDo
            {
                Id = Guid.NewGuid(),
                Title = toDoDto.Title,
                Description = toDoDto.Description,
                Status = ToDoStatus.NotStarted,
                Done = false,
                CreatedAt = DateTime.UtcNow
            };
            await toDosRepository.CreateAsync(toDo);

            return toDo;
        }

        public async Task<ToDo> DeleteAsync(Guid id)
        {
            ToDo toDo = await GetByIdAsync(id);
            await toDosRepository.DeleteAsync(toDo);
            return toDo;
        }

        public Task<List<ToDo>> GetAll()
        {
            return toDosRepository.GetAsync();
        }

        public Task<ToDo> GetByIdAsync(Guid id)
        {
            return toDosRepository.GetByIdAsync(id);
        }

        public async Task<ToDo> UpdateAsync(Guid id, UpdateToDoDto toDoDto)
        {
            ToDo dbToDo = await GetByIdAsync(id);

            bool hasStarted = toDoDto.Status == ToDoStatus.InProgress;
            bool hasFinished = toDoDto.Status == ToDoStatus.Completed;
            ToDo toDo = new ToDo
            {
                Id = dbToDo.Id,
                Title = toDoDto.Title,
                Description = toDoDto.Description,
                Status = toDoDto.Status,
                Done = hasFinished,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = dbToDo.CreatedAt
            };

            if (hasStarted)
            {
                toDo.StartedAt = DateTime.UtcNow;
            }

            await toDosRepository.UpdateAsync(toDo);
            return toDo;
        }
    }
}
