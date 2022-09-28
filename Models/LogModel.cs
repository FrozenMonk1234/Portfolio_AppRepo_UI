namespace Portfolio_AppRepo_UI.Models
{
    public class LogModel
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? InnerException { get; set; }
        public string? Source { get; set; }
        public DateTime Date { get; set; }
    }
}
