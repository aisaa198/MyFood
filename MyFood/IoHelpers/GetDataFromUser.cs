using System;

namespace MyFood.IoHelpers
{
    internal class GetDataFromUser : IGetDataFromUser
    {
        public string GetData(string messageToUser)
        {
            Console.Write(messageToUser);
            var input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                input = GetData("Give the data: ");
            }
            return input;
        }

        public int GetNumber(string messageToUser, int min, int max)
        {
            string input = GetData(messageToUser);
            int result;
            while (!int.TryParse(input, out result) || result < min || result > max)
            {
                input = GetData($"Give the correct data (number from {min} to {max}): ");
            }
            return result;
        }
    }
}
