﻿namespace _1670.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product>? Product { get; set; }
    }
}
