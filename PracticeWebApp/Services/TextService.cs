using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services.Interfaces;
using System.Text;

namespace PracticeWebApp.Services
{
    public class TextService : ITextService
    {
        char[] englishAlphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public async Task<string> ReturnProcessedString(string word) 
        {
            bool canCorrect = IsWordCorrect(word).Item1;
            if (!canCorrect)
            {
                return IsWordCorrect(word).Item2;
            }
            StringBuilder result = new StringBuilder();
            int lenght = word.Length;
            if (lenght % 2 == 0)
            {
                for (int i = lenght / 2 - 1; i >= 0; i--)
                {
                    result.Append(word[i]);
                }
                for (int i = lenght - 1; i >= lenght / 2; i--)
                {
                    result.Append(word[i]);
                }
            }
            else
            {
                for (int i = lenght - 1; i >= 0; i--)
                {
                    result.Append(word[i]);
                }
                result.Append(word);
            }
            return result.ToString();
        }


       
        public (bool, string) IsWordCorrect(string word)
        {
            List<string> unavalableSymbols = new List<string>();
           
            for (int i = 0; i < word.Length; i++)
            {
                if (!englishAlphabet.Contains(word[i]))
                {
                    if (word[i] == ' ')
                    {
                        unavalableSymbols.Add("пробел"); 
                    }
                    else if (word[i] == ',')
                    {
                        unavalableSymbols.Add("запятая");
                    }
                    else if (word[i] == '.')
                    {
                        unavalableSymbols.Add("точка");
                    }
                    else
                    {
                        unavalableSymbols.Add(word[i].ToString()); 
                    }
                }
            }
            string message = string.Empty;
            if (unavalableSymbols.Count > 0)
            {
                message = "Были введены неподходящие символы: ";
                message += string.Join(", ", unavalableSymbols);
                message += '.';
                return (false, message);
            }
            else
            {
                return (true, message);
            }
        }

    }
}
