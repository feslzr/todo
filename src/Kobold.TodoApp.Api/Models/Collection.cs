using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Models
{
    public class Collection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Todo> Todos { get; set; }
    }
}
