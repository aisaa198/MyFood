using MyFood.Common.Enums;
using MyFood.DAL.Data;
using MyFood.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyFood.DAL.Repositories
{
    public class RecipesRepository : IRecipesRepository
    {
        public Recipe AddRecipe(Recipe recipe)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                var newRecipe = dbContext.Recipes.Add(recipe);
                dbContext.SaveChanges();
                return newRecipe;
            }
        }

        public bool AddToFavorites(User user, Recipe recipe)
        {
            using (var dbContext = new MyFoodDbContext())
            {

                if (user != null && !dbContext.Users.Local.Contains(user) && recipe != null && !dbContext.Recipes.Local.Contains(recipe))
                {
                    var userFromBase = dbContext.Users.Include(x => x.Favourites).SingleOrDefault(x => x.Id == user.Id);
                    var recipeFromBase = dbContext.Recipes.Include(x => x.Users).Single(x => x.Id == recipe.Id);
                    dbContext.Users.Attach(userFromBase);
                    dbContext.Recipes.Attach(recipeFromBase);
                    userFromBase.Favourites.Add(recipeFromBase);
                    recipeFromBase.Users.Add(userFromBase);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public List<Recipe> GetRecipes(Category category)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                if (category == 0)
                {
                    return dbContext.Recipes.Include(x => x.Users).ToList();
                }
                else
                {
                    return dbContext.Recipes.Where(x => x.Category == category).Include(x => x.Users).ToList();
                }
            }
        } 
    }
}