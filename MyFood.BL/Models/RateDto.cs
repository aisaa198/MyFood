using System;

namespace MyFood.BL.Models
{
    public class RateDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public RecipeDto Recipe { get; set; }
        public UserDto User { get; set; }
    }
}
