namespace MyFood.MenuOptions
{
    internal interface IMenu
    {
        void AddOption(Option option);
        void InvokeCommand(string command);
        void PrintOptions();
    }
}