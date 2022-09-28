using System;

namespace CarService
{
    /// <summary>
    /// Класс-помощник для сохранения методов-расширения
    /// </summary>
    internal static class Helper
    {
        public static bool IsCorrectLFP(this string LFP)
        {
            if(string.IsNullOrWhiteSpace(LFP))
            {
                return false;
            }

            string[] splitLFP = LFP.Split(' ');
            if(splitLFP.Length != 3)
            {
                return false;
            }

            foreach(string word in splitLFP)
            {
                for(int i = 0, n = word.Length; i < n; i++)
                {
                    if (!char.IsLetter(word[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsNumber(this string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }
            if(number.Length != 6 || number.Split(' ').Length != 1)
            {
                return false;
            }
            if (!char.IsLetter(number[0]))
            {
                return false;
            }
            for(byte i = 1; i < 4; i++)
            {
                if (!char.IsDigit(number[i]))
                {
                    return false;
                }
            }
            for(byte i = 4; i < 6; i++)
            {
                if (!char.IsLetter(number[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
