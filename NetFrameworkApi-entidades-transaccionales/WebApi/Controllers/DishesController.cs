using Application.Endpoint.Commands;
using Application.Endpoint.DTOs;
using Application.Endpoint.Queries;
using Domain.Endpoint.Entities;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.App_Start.Filters;

namespace WebApi.Controllers
{
    public class DishesController : ApiController
    {
        private readonly IMediator mediator;
        public DishesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var dishes = await mediator.Send(new GetAllDishesQuery());
            return Ok(dishes);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateAsync(CreateDishDto dishDto)
        {
            //Dish dish = new Dish { Id = Guid.NewGuid() };
            Dish dish = await mediator.Send(new CreateDishCommand(dishDto, Guid.NewGuid()));
            var url = Url.Content("~/") + "api/dishes/" + dish.Id;
            return Created(url, dish);
        }

        [HttpPut]
        //[Route("{id}")]
        public async Task<IHttpActionResult> UpdateAsync(Guid id, UpdateDishDto dishDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Dish dish = await mediator.Send(new UpdateDishCommand(id, dishDto.Name, dishDto.Description, dishDto.Price, Guid.NewGuid()));
            return Ok(dish);
        }
    }
}
