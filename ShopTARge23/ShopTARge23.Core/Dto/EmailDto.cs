namespace ShopTARge23.Core.Dto
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<EmailAttachmentDto> Attachments { get; set; }
    }
    public class EmailAttachmentDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream Content { get; set; } // Faili sisu voog
    }
}