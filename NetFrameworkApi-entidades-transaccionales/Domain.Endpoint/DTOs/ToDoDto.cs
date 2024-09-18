using Domain.Endpoint.Entities;
//using System.ComponentModel.DataAnnotations;

namespace Domain.Endpoint.DTOs
{
    public class CreateToDoDto
    {
        //[Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateToDoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public ToDoStatus Status { get; set; }
    }
}
