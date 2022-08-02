using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Services;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly TokenService tokenService;
        private readonly ToDoService toDoService;

        public ToDoController(TokenService tokenService, ToDoService toDoService)
        {
            this.tokenService = tokenService;
            this.toDoService = toDoService;

        }


        [HttpGet]
        public IActionResult Get()
        {
            User user = tokenService.GetLoggedInUser();
            return Ok(toDoService.GetToDos(user));
        }



        [HttpPost]
        public IActionResult Post([FromBody] AddToDoDTO toDoDTO)
        {
            User user = tokenService.GetLoggedInUser();
            ResponseMessage responseMessage = toDoService.AddToDo(toDoDTO, user);
            return Ok(responseMessage);
        }

        [HttpPut("{toDoId}")]
        public IActionResult Put(int toDoId, [FromBody] UpdateToDoDTO toDoDTO)
        {
            User user = tokenService.GetLoggedInUser();
            ResponseMessage responseMessage = toDoService.UpdateToDo(toDoId, toDoDTO, user, out bool isValid);
            if (!isValid)
            {
                return BadRequest(responseMessage);
            }
            return Ok(responseMessage);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = tokenService.GetLoggedInUser();
        }
    }
}
