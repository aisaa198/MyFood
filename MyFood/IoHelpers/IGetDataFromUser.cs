namespace MyFood.IoHelpers
{
    internal interface IGetDataFromUser
    {
        string GetData(string messageToUser);
        int GetNumber(string messageToUser);
    }
}