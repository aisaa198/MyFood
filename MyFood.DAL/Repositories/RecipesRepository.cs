using MyFood.DAL.Data;
using MyFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.DAL.Repositories
{
    public class RecipesRepository
    {
        public Guid AddRecipe(Recipe recipe)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                var newRecipe = dbContext.Recipes.Add(recipe);
                dbContext.SaveChanges();
                return newRecipe.Id;
            }
        }

        public List<Recipe> ShowAllRecipes()
        {
            using (var dbContext = new MyFoodDbContext())
            {
                return dbContext.Recipes.ToList();
            }
        }
    }
}
