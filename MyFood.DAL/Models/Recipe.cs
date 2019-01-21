using MyFood.Common.Enums;
using System;
using System.Collections.Generic;

namespace MyFood.DAL.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public List<User> Users { get; set; }
    }
}
