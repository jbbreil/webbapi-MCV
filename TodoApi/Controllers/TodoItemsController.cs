using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    //// Home Route for API Web
    [Route("api/[controller]")]
    [ApiController]//Specifies that it is an API

    public class TodoItemsController : ControllerBase
    {
        //Initiering of Controller variable 
        private readonly TodoContex _context;

        public TodoItemsController(TodoContex context)
        {
            //Inizializering of controller
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            //Check if the object-ids are not the same
            if (id != todoItem.Id)
            {
                //Return status code 400
                return BadRequest();
            }
            //If IDs are same, set the state of TodoItems as modified.
            //Metoden SaveChangesAsync will modify the database. 
            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                //Try to save the changes 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    //Return status code 404
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //Return status code 204 to indicates that the request has succeeded
            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            //Find the object
            var todoItem = await _context.TodoItems.FindAsync(id);
            //If object do not exist, that is variable is not empty. 
            if (todoItem == null)
            {
                return NotFound();
            }
            //Remove object from database´s table
            _context.TodoItems.Remove(todoItem);
            //Save the changes in database
            await _context.SaveChangesAsync();

            //Success status code, return the canceled object
            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
