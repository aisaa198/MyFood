using MyFood.IoHelpers;
using MyFood.MenuOptions;
using System;

namespace MyFood
{
    class ProgramLoop
    {
        private readonly IUserManagementsService _userManagementsService;
        private readonly IMenu _menu;
        private bool _exit = false;

        public ProgramLoop(IUserManagementsService userManagementsService, IMenu menu)
        {
            _userManagementsService = userManagementsService;
            _menu = menu;
            AddMenuOptions();
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

        private void AddMenuOptions()
        {
            _menu.AddOption(new Option("1", "Log in", _userManagementsService.LogIn));
            _menu.AddOption(new Option("2", "Register", _userManagementsService.AddUser));
            _menu.AddOption(new Option("3", "Exit", Exit));
        }

        private void Exit()
        {
            _exit = true;
        }
    }
}
