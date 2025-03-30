using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Server.Services;

namespace TodoApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }
       
        // GET: API/Todos
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<Tasks> todos = await _todoService.GetAsync();
            return Ok(todos);
        }

        // POST: API/Todos/Create
        [HttpPost]
        //[Route("todos/create")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] Tasks task)
        {
            try
            {
                // Add the new todo to the list
                await _todoService.CreateAsync(task);
                List<Tasks> todos = await _todoService.GetAsync();
                return Ok(todos);
            }
            catch
            {   
                // todo: Log the error (uncomment the line below after adding a logger)
                return Problem("An error occurred while creating the todo item.");
            }
        }
        // Put: API/Todos/Edit/5
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromBody] Tasks task)
        {
            try
            {

                var existingTodo = await _todoService.GetAsync(task.Id);
                if (existingTodo == null)
                {
                    return NotFound();
                }
                task.Id = existingTodo.Id;
                await _todoService.UpdateAsync(task.Id, task);

                List<Tasks> todos = await _todoService.GetAsync();
                return Ok(todos);
            }
            catch
            {
                return Problem("An error occurred while editing the todo item. Please ensure the ID is valid and the title is not empty.");
            }
        }

        [HttpDelete]
        // Delete: API/Todos/Delete/5
        public async Task<ActionResult> Delete([FromBody] Tasks task)
        {
            var todo = await _todoService.GetAsync(task.Id);
            if (todo == null)
            {
                return NotFound();
            }
            await _todoService.RemoveAsync(task.Id);

            List<Tasks> todos = await _todoService.GetAsync();
            return Ok(todos);
        }

        
    }
}
