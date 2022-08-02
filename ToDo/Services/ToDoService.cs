using Microsoft.EntityFrameworkCore;
using ToDo.Database;
using ToDo.Models;
using ToDo.Models.DTOs;

namespace ToDo.Services
{
    public class ToDoService
    {
        private AppDbContext database;

        public ToDoService(AppDbContext database)
        {
            this.database = database;
        }

        public ResponseMessage AddToDo(AddToDoDTO addToDoDTO, User user)
        {
            ToDoTask toDoTask = new ToDoTask(addToDoDTO.Description, user.Id);

            database.ToDoTasks.Add(toDoTask);
            database.SaveChanges();
            return new ResponseMessage("Task added");
        }

        public ResponseMessage DeleteToDo(int taskId, User user, out bool isValid)
        {
            ToDoTask task = GetToDoTaskById(taskId);
            if (!user.Tasks.Contains(task))
            {
                isValid = false;
                return new ResponseMessage("invalid task");
            }
            database.ToDoTasks.Remove(task);
            database.SaveChanges();
            isValid = true;
            return new ResponseMessage("Task deleted");
        }

        public List<ToDoDTO> GetToDos(User user)
        {
            return user.Tasks.Select(t => new ToDoDTO(t.Id, t.Description, t.IsDone)).ToList();
        }
        public ResponseMessage UpdateToDo(int taskId, UpdateToDoDTO toDoDTO, User user, out bool isValid)
        {
            ToDoTask task = GetToDoTaskById(taskId);
            if (!user.Tasks.Contains(task))
            {
                isValid = false;
                return new ResponseMessage("invalid task");
            }
            task.Description = toDoDTO.Description;
            task.IsDone = toDoDTO.IsDone;
            database.ToDoTasks.Update(task);
            database.SaveChanges();
            isValid = true;
            return new ResponseMessage("Task Updated");
        }
        public ToDoTask GetToDoTaskById(int taskId)
        {
            return database.ToDoTasks.FirstOrDefault(t => t.Id == taskId);
        }
    }
}
