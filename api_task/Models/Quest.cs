namespace api_task.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual State State { get; set;}
        public virtual Priority Priority { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
