namespace ToDo.Models
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public ResponseMessage(string message)
        {
            Message = message;
        }
    }
}
