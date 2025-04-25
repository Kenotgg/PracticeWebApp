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
            Dictionary<char, int> unavailableSymbolsCount = new Dictionary<char, int>();

            for (int i = 0; i < word.Length; i++)
            {
                if (!englishAlphabet.Contains(word[i]))
                {
                    if (unavailableSymbolsCount.ContainsKey(word[i]))
                    {
                        unavailableSymbolsCount[word[i]]++;//Прибавим значение к int по char.
                    }
                    else
                    {
                        unavailableSymbolsCount[word[i]] = 1;//При первом нахождении делаем значение = 1.
                    }
                }
            }

            string message = string.Empty;
            if (unavailableSymbolsCount.Count > 0)
            {
                message = "Были введены некорректные символы: ";
                List<string> symbolCounts = new List<string>();
                foreach (var pair in unavailableSymbolsCount)
                {
                    string symbolDescription;
                    if (pair.Key == ' ')
                    {
                        symbolDescription = $"пробел - {pair.Value} раз";
                    }
                    else if (pair.Key == ',')
                    {
                        symbolDescription = $"запятая - {pair.Value} раз";
                    }
                    else if (pair.Key == '.')
                    {
                        symbolDescription = $"точка - {pair.Value} раз";
                    }
                    else
                    {
                        symbolDescription = $"{pair.Key} - {pair.Value} раз";
                    }
                    symbolCounts.Add(symbolDescription);
                }

                message += string.Join(", ", symbolCounts);
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
