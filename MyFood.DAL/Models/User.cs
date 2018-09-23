using System;
using System.Collections.Generic;

namespace MyFood.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Recipe> Favourites { get; set; }
    }
}
