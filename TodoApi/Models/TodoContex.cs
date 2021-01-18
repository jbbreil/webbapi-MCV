                                    
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContex : DbContext
    {
        public TodoContex(DbContextOptions<TodoContex> options)

        //Anrop till konstruktorn som finns i DbContext som i huvudsak skapar TodoItems-tabellen i databasen med schemat<TodoItem>
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } //TodoItems-tabellen 
    }
}
