using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1670.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        [NotMapped]
        public IFormFile FileImage { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Order>? Order { get; set;}
    }
}
