namespace ToDo.Models
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }

        public ToDoDTO(int id, string description, bool isDone)
        {
            Id = id;
            Description = description;
            IsDone = isDone;
        }
    }
}
