namespace API.Models
{
    public class CreateTaskModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
    }
}
