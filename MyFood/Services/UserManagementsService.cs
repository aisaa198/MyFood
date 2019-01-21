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
    public class UserManagementsService : IUserManagementsService
    {
        private readonly IMenu _menu;
        private readonly IRecipesService _recipesService;
        private readonly IRatesService _ratesService;
        private readonly IGetDataFromUser _getDataFromUser;
        private readonly IUserService _userService;
        private bool _exit = false;
        public UserDto _loggedUser = null;

        public UserManagementsService(IMenu menu, IRecipesService recipesService, IRatesService ratesService, IGetDataFromUser getDataFromUser, IUserService userService)
        {
            _menu = menu;
            _recipesService = recipesService;
            _ratesService = ratesService;
            _getDataFromUser = getDataFromUser;
            _userService = userService;
            AddMenuOptions();
        }

        private void AddMenuOptions()
        {
            _menu.AddOption(new Option("1", "Add recipe", AddRecipe));
            _menu.AddOption(new Option("2", "Show all recipes", ShowAllReceipes));
            _menu.AddOption(new Option("3", "Show recipes from one category", ShowRecipesInCategory));
            _menu.AddOption(new Option("4", "Search recipes by ingredients", SearchRecipes));
            _menu.AddOption(new Option("5", "Log out", LogOut));
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

        public void AddUser()
        {
            var login = _getDataFromUser.GetData("Provide login: ");
            var users = _userService.GetAllUsers();
            while (users.Any(x => x.Login == login))
            {
                Console.WriteLine("Login already exists");
                login = _getDataFromUser.GetData("Provide login: ");
            }
            var name = _getDataFromUser.GetData("Enter your name: ");
            var password = _getDataFromUser.GetData("Provide password: ");
            var newUser = new UserDto()
            {
                Id = Guid.NewGuid(),
                Login = login,
                Password = password,
                Name = name,
                Favourites = new List<RecipeDto>()
            };
            var result = _userService.RegisterUser(newUser);
            ShowResult(result);
        }

        public void LogIn()
        {
            var login = _getDataFromUser.GetData("Login: ");
            var password = _getDataFromUser.GetData("Password: ");

            var success = _userService.LogIn(login, password);

            if (success == null)
            {
                Console.WriteLine("Wrong login or password!");
            }
            else
            {
                _loggedUser = success;
                _exit = false;
                Console.WriteLine($"Hello {_loggedUser.Name}! Choose what you want to do:");
                Run();
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

            var rate = _ratesService.CountRate(recipe.Id);
            Console.WriteLine($"Rate: {rate}");
            AddRate(recipe);
            AddToFavorites(recipe);
        }

        private void UpdateLoggedUser()
        {
            _loggedUser = _userService.GetUserByLogin(_loggedUser.Login);
        }

        private void AddToFavorites(RecipeDto recipe)
        {
            if (_loggedUser != null && !_loggedUser.Favourites.Any(x => x.Id == recipe.Id))
            {
                var choice = _getDataFromUser.GetData("Do you want to add this recipe to favorites? Y/N");

                if (choice.ToUpper() == "Y")
                {
                    var success = (_recipesService.AddToFavorites(_loggedUser, recipe))? new object() : null;
                    ShowResult(success);
                    UpdateLoggedUser();
                };
            }
        }

        private void ShowResult(object result)
        {
            Console.WriteLine("The operation " + ((result == null) ? "failed!" : "was succesful!"));
        }

        private void AddRate(RecipeDto recipe)
        {
            var rates = _ratesService.GetRates(recipe.Id);

            if( _loggedUser != null)
            {
                var loggedUserRate = rates.SingleOrDefault(x => x.User.Id == _loggedUser.Id);
                if (loggedUserRate != null)
                {
                    Console.WriteLine($"Your rate: {loggedUserRate.Score}");
                }
                else
                {
                    var choice = _getDataFromUser.GetData("Do you want to rate this recipe? Y/N");

                    if (choice.ToUpper() == "Y")
                    {
                        var score = _getDataFromUser.GetNumber("Your rate (0-10): ", 0, 10);
                        var rate = new RateDto
                        {
                            Id = Guid.NewGuid(),
                            Score = score,
                            User = _loggedUser,
                            Recipe = recipe
                        };
                        var result = _ratesService.AddRate(rate);
                        ShowResult(result);
                        UpdateLoggedUser();
                    };
                }
            }  
        }

        private void SearchRecipes()
        {
            var listOfIngredients = SetIngredients();
            var recipes = _recipesService.SearchRecipes(listOfIngredients);
            PresentRecipes(recipes);
        }

        private void PresentRecipes(IList<RecipeDto> recipes)
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

            int choice = _getDataFromUser.GetNumber("Choose recipe: ", 1, recipes.Count);
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

        private string[] SetIngredients()
        {
            var numberOfIngredients = _getDataFromUser.GetNumber("How many ingredients? ", 1, 10);
            var listOfIngredients = new string[numberOfIngredients];

            for (var i = 1; i <= numberOfIngredients; i++)
            {
                var ingredient = _getDataFromUser.GetData($"{i}: ");
                listOfIngredients[i - 1] = ingredient;
            }
            return listOfIngredients;
        }

        private void AddRecipe()
        {
            var category = ChooseCategory();
            var name = _getDataFromUser.GetData("Give name: ");
            var listOfIngredients = SetIngredients();
            var directions = _getDataFromUser.GetData("Give directions: ");

            var newRecipe = new RecipeDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Category = category,
                ListOfIngredients = listOfIngredients,
                Directions = directions,
                Users = new List<UserDto>()
            };

            var result = _recipesService.AddRecipe(newRecipe);
            ShowResult(result);
        }

        private void LogOut()
        {
            _exit = true;
            _loggedUser = null;
        }   
    }
}
