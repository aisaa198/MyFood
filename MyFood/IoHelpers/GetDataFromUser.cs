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

        public int GetNumber(string messageToUser)
        {
            string input = GetData(messageToUser);
            int result;
            while (!int.TryParse(input, out result) || result < 0)
            {
                input = GetData($"Give the correct data (number greater than 0): ");
            }
            return result;
        }
    }
}
