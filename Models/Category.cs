using System.ComponentModel.DataAnnotations;

namespace Nimap_Project.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName {  get; set; }
    }
}
