namespace ToDo.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<ToDoDTO> Tasks { get; set; }
    }
}
