using AutoMapper;
using MyFood.BL.Models;
using MyFood.Common.Enums;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using MyFood.BL.Services.Interfaces;
using System;

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

        public List<RecipeDto> AddExemplaryRecipes()
        {
            var newRecipes = new List<Recipe>
            {
                new Recipe
                {
                Id = Guid.NewGuid(),
                Name = "Francuskie paluchy pomidorowe",
                Category = Category.Snack,
                Ingredients = "ciasto francuskie,koncentrat pomidorowy,jajka,sezam,oregano",
                Directions = "Ciasto francuskie rozwiń, posmaruj koncentratem pomidorowym i posyp oregano. Pokrój na paski i umieść je jeden na drugim. Skręcaj spiralnie. Posmaruj rozmąconym jajkiem i posyp sezamem. Piecz w 200 stopniach na złoty kolor."
                },
                new Recipe
                {
                Id = Guid.NewGuid(),
                Name = "Babeczka z kubeczka",
                Category = Category.Dessert,
                Ingredients = "mleko,jajka,mąka,proszek do pieczenia,olej,cukier,kakao",
                Directions = "Wszystkie mokre składniki połączyć za pomocą widelca w kubeczku, następnie dodać składniki suche i wymieszać. Piec około 3-4 minuty w mikrofalówce na najwyższej mocy."
                },
                new Recipe
                {
                Id = Guid.NewGuid(),
                Name = "Muffiny bananowe",
                Category = Category.Dessert,
                Ingredients = "mąka,mleko,banany,jajka,olej,proszek do pieczenia",
                Directions = "Banany, mleko, jajka i olej zmiksuj na gładką masę. Dodaj mąkę wymieszaną z proszkiem do pieczenia, delikatnie wymieszaj. Foremki wypełnij do 3/4 wysokości. Piecz w 180 stopniach przez około 15 minut."
                },
                new Recipe
                {
                Id = Guid.NewGuid(),
                Name = "Zapiekanka makaronowa",
                Category = Category.MainCourse,
                Ingredients = "makaron,masło,mleko,mąka,brokuły,kurczak,ser,sól,pieprz",
                Directions = "Makaron i brokuły ugotuj al dente. Kurczaka pokrój, przypraw solą i pieprzem i smaż przez chwilę. z masła, mleka i mąki zrób sos beszamelowy. Wymieszaj wszystko w żaroodpornym naczyniu, posyp startym serem. Piecz około 30 minut w 180 stopniach."
                }
            };

            var addedRecipes = new List<RecipeDto>();
            foreach(var recipe in newRecipes)
            {
                addedRecipes.Add(AddRecipe(_mapper.Map<RecipeDto>(recipe)));
            }

            return addedRecipes;   
        }

        public RecipeDto AddRecipe(RecipeDto recipeDto)
        {
            if (recipeDto == null) return null;
            recipeDto.Id = Guid.NewGuid();
            var recipe = _mapper.Map<Recipe>(recipeDto);
            var addedRecipe = _recipesRepository.AddRecipe(recipe);
            return _mapper.Map<RecipeDto>(addedRecipe);
        }

        public bool AddToFavorites(UserDto userDto, RecipeDto recipeDto)
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
                if (counter > 0)
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