namespace ToDo.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ToDoTask()
        {
        }

        public ToDoTask(string description, int userId)
        {
            Description = description;
            UserId = userId;
        }
    }
}
