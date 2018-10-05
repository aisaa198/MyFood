using MyFood.DAL.Data;
using MyFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.DAL.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        public Guid AddRate(Rate rate)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                var newRate = dbContext.Rates.Add(rate);
                dbContext.SaveChanges();
                return newRate.Id;
            }
        }

        public List<Rate> GetRate(Guid id)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                return dbContext.Rates.Where(x => x.User.Id == id).ToList();
            }
        }
    }
}
