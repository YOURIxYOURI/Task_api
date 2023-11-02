namespace api_task.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual Quest Task { get; set; }
    }
}
