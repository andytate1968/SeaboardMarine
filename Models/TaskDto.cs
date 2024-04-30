using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Title.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please enter a Description.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter a Due Date.")]
        public DateTime DueDate { get; set;}
        public bool IsCompleted { get; set;}
        public string ListOfTags { get; set; } = string.Empty;
        public List<TaskTagDto> TaskTags { get; set; }
        
    }
}
