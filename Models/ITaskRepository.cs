using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deadDraft.Models
{
    public interface ITaskRepository
    {
        ToDo GetTask(int Id);
        IEnumerable<ToDo> GetAllTasks();
        ToDo Add(ToDo toDo);
        ToDo Update(ToDo toDoChanges);
        ToDo Delete(int Id);
    }
}
