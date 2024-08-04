using System.ComponentModel.DataAnnotations;

namespace LOSTONTHEJOURNEY.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
