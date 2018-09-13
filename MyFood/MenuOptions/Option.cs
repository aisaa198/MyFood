using System;

namespace MyFood.MenuOptions
{
    internal class Option
    {
        public string Number { get; }
        public string Command { get; }
        public Action Callback { get; }

        public Option(string number, string command, Action callback)
        {
            Number = number;
            Command = command;
            Callback = callback;
        }
    }
}
