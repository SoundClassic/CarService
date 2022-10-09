using System;
using System.Linq;
using System.Collections.Generic;

namespace CarService
{
    /// <summary>
    /// Класс-помощник для сохранения методов-расширения
    /// </summary>
    public static class Helper
    {
        public static bool IsCorrectLFP(this string LFP)
        {
            if(string.IsNullOrWhiteSpace(LFP))
            {
                return false;
            }

            string[] splitLFP = LFP.Split(' ');
            List<string> splitLFPDeletedWhiteSpace = new List<string>();

            foreach(string word in splitLFP)
            {
                if(word != "")
                {
                    splitLFPDeletedWhiteSpace.Add(word);
                }
            }

            if(splitLFPDeletedWhiteSpace.Count != 3)
            {
                return false;
            }

            foreach(string word in splitLFPDeletedWhiteSpace)
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

        public static bool IsNumberBid(this string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }
            if(number.Length != 8)
            {
                return false;
            }
            for(byte i = 0; i < 8; i++)
            {
                if (i < 4)
                {
                    if (!char.IsLetter(number[i]))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!char.IsDigit(number[i]))
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public static string GenerateNumberBid()
        {
            Random random = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
            int num = random.Next(1000, 10000);
            string word = "";
            for (byte i = 0; i < 4; i++)
            {
                word += (char)random.Next('A', 'Z' + 1);
            }
            return word + num;
        }
    }
}
