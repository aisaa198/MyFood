using MyFood.DAL.Data;
using MyFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyFood.DAL.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        public Rate AddRate(Rate rate)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                var user = dbContext.Users.SingleOrDefault(x => x.Id == rate.User.Id);
                rate.User = user;

                var recipe = dbContext.Recipes.SingleOrDefault(x => x.Id == rate.Recipe.Id);
                rate.Recipe = recipe;

                if (dbContext.Rates.SingleOrDefault(x => x.User.Id == user.Id && x.Recipe.Id == recipe.Id) != null)
                {
                    return null;
                }

                var newRate = dbContext.Rates.Add(rate);
                dbContext.SaveChanges();
                return newRate;
            }
        }

        public List<Rate> GetRates(Guid recipeId)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                return dbContext.Rates.Where(x => x.Recipe.Id == recipeId).Include(x => x.User).ToList();
            }
        }
    }
}
