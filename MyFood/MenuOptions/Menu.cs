using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.MenuOptions
{
    internal class Menu
    {
        private readonly List<Option> _options = new List<Option>();
        public bool Exit { get; set; }
        public string ExitCommand { get; set; }

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
            if (!string.IsNullOrWhiteSpace(ExitCommand))
                Console.WriteLine(ExitCommand);
        }

        public void InvokeCommand(string command)
        {
            var menuOption = _options.SingleOrDefault(o => o.Number == command);
            //menuOption.Callback();
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

