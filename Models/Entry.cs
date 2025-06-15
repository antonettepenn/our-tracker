namespace OurTracker.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // which type of thing tracked "date-idea", "bucketlist-date", "show", "note"
        public string Description { get; set; }
        public DateTime CreatedAt {get; set; } 
    }
}