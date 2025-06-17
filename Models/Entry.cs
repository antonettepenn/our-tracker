namespace OurTracker.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; } // "todo", "show", "bucket", "date", "note"
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsCompleted { get; set; } 
    }
}