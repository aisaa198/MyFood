using System;

namespace MyFood.DAL.Models
{
    public class Rate
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}
