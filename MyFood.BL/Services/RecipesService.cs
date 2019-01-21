using AutoMapper;
using MyFood.BL.Models;
using MyFood.Common.Enums;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using MyFood.BL.Services.Interfaces;

namespace MyFood.BL.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IMapper _mapper;

        public RecipesService(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }
        
        public RecipeDto AddRecipe(RecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            var addedRecipe = _recipesRepository.AddRecipe(recipe);
            return _mapper.Map<RecipeDto>(addedRecipe);
        }

        public bool AddToFavorites(UserDto userDto,  RecipeDto recipeDto)
        {
            var user = _mapper.Map<User>(userDto);
            var recipe = _mapper.Map<Recipe>(recipeDto);

            return _recipesRepository.AddToFavorites(user, recipe);
        }

        public virtual List<RecipeDto> GetRecipes(Category category)
        {
            return _recipesRepository.GetRecipes(category).Select(recipe => _mapper.Map<RecipeDto>(recipe)).ToList();
        }

        public List<RecipeDto> SearchRecipes(string[] listOfIngredients)
        {
            var allRecipes = GetRecipes(0);
            var foundRecipes = new Dictionary<RecipeDto, int>();
            var counter = 0;

            if (listOfIngredients == null || listOfIngredients.Length == 0) return allRecipes;
            foreach (var recipe in allRecipes)
            {
                foreach (var ingredient in recipe.ListOfIngredients)
                {
                    foreach (var element in listOfIngredients)
                    {
                        if (ingredient == element)
                        {
                            counter++;
                        }
                    }
                }
                if(counter > 0)
                {
                    foundRecipes.Add(recipe, counter);
                    counter = 0;
                }
            }
            var orderedRecipes = foundRecipes.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value); ;
            var listOfRecipes = orderedRecipes.Keys.ToList();
            return listOfRecipes;
        }
    }
}