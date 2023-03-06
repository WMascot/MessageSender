namespace MessageSender.Console.Models
{
    public class MailData
    {
        public List<string> To { get; }
        public string From { get; }
        public string DisplayName { get; }
        public string? ReplyTo { get; }
        public string? ReplyToName { get; }
        public string? Subject { get; }
        public string Body { get; }
        public MailData(List<string> to, string from, string displayName, string body, string? replyTo = null, string? replyToName = null, string? subject = null)
        {
            To = to;
            From = from;
            DisplayName = displayName;
            Body = body;
            ReplyTo = replyTo;
            ReplyToName = replyToName;
            Subject = subject;
        }
    }
}
