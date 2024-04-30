using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public IEnumerable<TaskTag> TaskTags { get; set; }
    }
}
