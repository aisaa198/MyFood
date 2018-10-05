using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.MenuOptions
{
    internal class Menu : IMenu
    {
        private readonly List<Option> _options = new List<Option>();

        public void AddOption(Option option)
        {
            _options.Add(option);
        }

        public void PrintOptions()
        {
            Console.WriteLine("Options:");
            foreach (var option in _options)
            {
                Console.WriteLine($"{option.Number} - {option.Command}");
            }
        }

        public void InvokeCommand(string command)
        {
            var menuOption = _options.SingleOrDefault(o => o.Number == command);
            if (menuOption == null)
            {
                Console.WriteLine($"Command {command} does not exist.");
            }
            else
            {
                menuOption.Callback();
            }
        }
    }
}

