using System;
using System.Collections.Generic;
using System.Linq;
using MyFood.BL.Models;
using MyFood.BL.Services;
using MyFood.BL.Services.Interfaces;
using MyFood.Common.Enums;
using MyFood.IoHelpers;
using MyFood.MenuOptions;

namespace MyFood
{
    class ProgramLoop
    {
        private readonly IMenu _menu;
        private readonly IRecipesService _recipesService;
        private readonly IRatesService _ratesService;
        private readonly IGetDataFromUser _getDataFromUser;
        private bool _exit = false;

        public ProgramLoop(IMenu menu, IRecipesService recipesService, IRatesService ratesService, IGetDataFromUser getDataFromUser)
        {
            _menu = menu;
            _recipesService = recipesService;
            _ratesService = ratesService;
            _getDataFromUser = getDataFromUser;
            _menu.AddOption(new Option("1", "Add receipe", AddReceipe));
            _menu.AddOption(new Option("2", "Show all recipes", ShowAllReceipes));
            _menu.AddOption(new Option("3", "Show recipes from one category", ShowRecipesInCategory));
            _menu.AddOption(new Option("4", "Search recipes by ingredients", SearchRecipes));
            _menu.AddOption(new Option("5", "Exit", Exit));
        }

        internal void Run()
        {
            while (!_exit)
            {
                _menu.PrintOptions();
                Console.Write("Type commad: ");
                var command = Console.ReadLine();
                _menu.InvokeCommand(command);
            }
        }

        private void ShowRecipe(RecipeDto recipe)
        {
            Console.WriteLine(recipe.Name.ToUpper());
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.ListOfIngredients)
            {
                Console.WriteLine($"* {ingredient}");
            }
            Console.WriteLine("Directions:");
            Console.WriteLine(recipe.Directions);
            Console.WriteLine("----------------------------------");

            var rate = _ratesService.GetRate(recipe.Id);
            Console.WriteLine($"Rate: {rate}");
        }

        private void SearchRecipes()
        {
            var listOfIngredients = SetIngredients();
            var recipes = _recipesService.SearchRecipes(listOfIngredients);
            PresentRecipes(recipes);
        }

        private void PresentRecipes(List<RecipeDto> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("Nothing to show!");
                return;
            }

            foreach (var recipe in recipes)
            {
                Console.Write($"{recipes.IndexOf(recipe)+1}. {recipe.Name} ({recipe.Category}) - ");
                foreach (var ingredient in recipe.ListOfIngredients)
                {
                    if (ingredient.Equals(recipe.ListOfIngredients.Last()))
                    {
                        Console.Write($"{ingredient}");
                    }
                    else
                    {
                        Console.Write($"{ingredient},");
                    }
                }
                Console.WriteLine();
            }

            var choice = _getDataFromUser.GetNumber("Choose recipe: ");
 
            ShowRecipe(recipes[choice - 1]);
        }

        private void ShowAllReceipes()
        {
            var recipes = _recipesService.GetRecipes(0);
            PresentRecipes(recipes);
        }

        private void ShowRecipesInCategory()
        {
            var category = ChooseCategory();
            var recipes = _recipesService.GetRecipes(category);
            PresentRecipes(recipes);
        }

        private Category ChooseCategory()
        {
            Console.WriteLine("Choose the category: ");
            foreach (var option in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)option} - {option}");
            }
            int.TryParse(Console.ReadLine(), out int numberOfEnumCategory);
            Enum.TryParse<Category>(Enum.GetName(typeof(Category), numberOfEnumCategory), out Category category);
            return category;
        }

        private List<string> SetIngredients()
        {
            var numberOfIngredients = _getDataFromUser.GetNumber("How many ingredients? ");
            var listOfIngredients = new List<string>();

            for (var i = 1; i <= numberOfIngredients; i++)
            {
                var ingredient = _getDataFromUser.GetData($"{i}: ");
                listOfIngredients.Add(ingredient);
            }
            return listOfIngredients;
        }

        private void AddReceipe()
        {
            var category = ChooseCategory();
            var nameOfDish = _getDataFromUser.GetData("Give name: ");
            var listOfIngredients = SetIngredients();
            var directions = _getDataFromUser.GetData("Give directions: ");

            var newRecipe = new RecipeDto
            {
                Id = Guid.NewGuid(),
                Name = nameOfDish,
                Category = category,
                ListOfIngredients = listOfIngredients,
                Directions = directions
            };

            _recipesService.AddRecipe(newRecipe);
        }

        private void Exit()
        {
            _exit = true;
        }   
    }
}
