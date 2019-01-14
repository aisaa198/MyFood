namespace MyFood.IoHelpers
{
    public interface IGetDataFromUser
    {
        string GetData(string messageToUser);
        int GetNumber(string messageToUser, int min, int max);
    }
}