namespace MyFood.MenuOptions
{
    public interface IMenu
    {
        void AddOption(Option option);
        void InvokeCommand(string command);
        void PrintOptions();
    }
}