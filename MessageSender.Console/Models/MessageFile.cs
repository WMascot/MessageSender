namespace MessageSender.Console.Models
{
    public class MessageFile
    {
        public string Directory { get; set; }
        public DateOnly SendingDate { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public string FullName { get; set; }
        public string Destination { get; set; }
        public MessageStatus MessageStatus { get; set; }
    }
    public enum MessageStatus
    {
        READY,
        SENT
    }
}
