using System;
using System.Collections.Generic;

namespace MyFood.BL.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<RecipeDto> Favourites { get; set; }
    }
}
