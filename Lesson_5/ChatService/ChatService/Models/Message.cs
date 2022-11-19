namespace ChatService.Models
{
    public class Message
    {
        public int FromId { get; set; }
        public int ToId { get; set; }  
        public string? Text { get; set; }
    }
}
