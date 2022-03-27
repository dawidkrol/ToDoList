using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary.Models
{
    public class TaskDbModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatingDate { get; set; }
        public StatusDbModel Status { get; set; }
    }
}
