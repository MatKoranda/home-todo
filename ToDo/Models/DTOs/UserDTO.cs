namespace ToDo.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ToDoTask> Tasks { get; set; }
    }
}
