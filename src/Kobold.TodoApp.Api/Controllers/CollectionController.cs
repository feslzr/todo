using Kobold.TodoApp.Api.Models;
using Kobold.TodoApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly CollectionService _collectionService;

        public CollectionController(CollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public IEnumerable<Collection> Get()
        {
            return _collectionService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Collection> Get([FromRoute] int id)
        {
            return _collectionService.Get(id);
        }

        [HttpPost]
        public ActionResult<Collection> Create([FromBody] CollectionViewModel model)
        {
            var collection = _collectionService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = collection.Id }, collection);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            _collectionService.Remove(id);
            return NoContent();
        }

        [HttpGet("{collectionId}/todos")]
        public IEnumerable<Todo> GetTodos([FromRoute] int collectionId)
        {
            return _collectionService.GetTodos(collectionId);
        }

        [HttpGet("{collectionId}/todos/{todoId}")]
        public ActionResult<Todo> GetTodo([FromRoute] int collectionId, [FromRoute] int todoId)
        {
            return _collectionService.GetTodo(collectionId, todoId);
        }

        [HttpPost("{collectionId}/todos")]
        public ActionResult<Todo> AddTodo([FromRoute] int collectionId, [FromBody] TodoViewModel model)
        {
            var todo = _collectionService.AddTodo(collectionId, model);
            return CreatedAtAction(nameof(GetTodo), new { collectionId, todoId = todo.Id }, todo);
        }

        [HttpPut("{collectionId}/todos/{todoId}")]
        public ActionResult<Todo> UpdateTodo([FromRoute] int collectionId, [FromRoute] int todoId, [FromBody] TodoViewModel model)
        {
            return _collectionService.UpdateTodo(collectionId, todoId, model);
        }

        [HttpDelete("{collectionId}/todos/{todoId}")]
        public IActionResult RemoveTodo([FromRoute] int collectionId, [FromRoute] int todoId)
        {
            _collectionService.RemoveTodo(collectionId, todoId);
            return NoContent();
        }
    }
}