using System;
using MyFood.MenuOptions;

namespace MyFood
{
    class ProgramLoop
    {
        private readonly Menu _menu;
        private bool exit = false;

        public ProgramLoop()
        {
            _menu = new Menu();
            _menu.AddOption(new Option("1", "Add receipe", AddReceipe));
            _menu.AddOption(new Option("2", "Exit", Exit));

        }

        private void AddReceipe()
        {
            Console.WriteLine("Receipe added.");
        }

        private void Exit()
        {
            exit = true;
        }

        internal void Run()
        {
            while (!exit)
            {
                _menu.PrintOptions();
                Console.Write("Type commad: ");
                var command = Console.ReadLine();
                _menu.InvokeCommand(command);
            }
        }
    }
}
