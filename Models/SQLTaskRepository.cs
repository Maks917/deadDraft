using System.Collections.Generic;

namespace deadDraft.Models
{
    public class SQLTaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;

        public SQLTaskRepository(AppDbContext context)
        {
            this.context = context;
        }

        public ToDo Add(ToDo toDo)
        {
            context.ToDos.Add(toDo);
            context.SaveChanges();
            return toDo;
        }

        public ToDo Delete(int Id)
        {
            ToDo toDo = context.ToDos.Find(Id);
            if (toDo != null)
            {
                context.ToDos.Remove(toDo);
                context.SaveChanges();
            }
            return toDo;
        }

        public IEnumerable<ToDo> GetAllTasks()
        {
            return context.ToDos;
        }

        public ToDo GetTask(int Id)
        {
            return context.ToDos.Find(Id);
        }

        public ToDo Update(ToDo toDoChanges)
        {
            var toDo = context.ToDos.Attach(toDoChanges);
            toDo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return toDoChanges;
        }
    }
}
