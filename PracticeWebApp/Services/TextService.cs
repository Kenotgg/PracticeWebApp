using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services.Interfaces;
using System.Linq;
using System.Text;

namespace PracticeWebApp.Services
{
    public class TextService : ITextService
    {
        char[] englishAlphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] vowelLetters = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        public async Task<string> ReturnProcessedString(string word)
        {
            bool isCorrect = IsWordCorrect(word).Item1;
            if (!isCorrect)
            {
                return "Самая длинная подстрока: " + FindLongestVowelSubstring(word.ToString()) + ", " + IsWordCorrect(word).Item2;
            }
            //новый функционал.
            
            string processedWord = string.Empty;
            int lenght = word.Length;
            if (lenght % 2 == 0)
            {
                for (int i = lenght / 2 - 1; i >= 0; i--)
                {
                    processedWord += word[i];
                }
                for (int i = lenght - 1; i >= lenght / 2; i--)
                {
                    processedWord += word[i];
                }
            }
            else
            {
                for (int i = lenght - 1; i >= 0; i--)
                {
                    processedWord += word[i];
                }
                processedWord += word;
            }

            List<string> symbolCounts = new List<string>();
            foreach (var pair in GetSymbolsCountDictionary(processedWord,true))
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
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.Append("Обработанная строка: ");
            resultMessage.Append("\n");
            resultMessage.Append(processedWord + ".");
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            resultMessage.Append("Cколько повторений символов встречается: ");
            resultMessage.Append("\n");
            resultMessage.Append(string.Join(", ", symbolCounts) + ".");
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            if (FindLongestVowelSubstring(processedWord.ToString()).Count() > 0)
            {
                resultMessage.Append("Cамая длинная подстрока: ");
                resultMessage.Append("\n");
                resultMessage.Append(FindLongestVowelSubstring(processedWord.ToString()) + ".");
            }
            return resultMessage.ToString();
        }


        private Dictionary<char,int> GetSymbolsCountDictionary(string word, bool containsEnglishAlpabet) 
        {
            Dictionary<char, int> symbolsCount = new Dictionary<char, int>();
            for (int i = 0; i < word.Length; i++)
            {

                if (englishAlphabet.Contains(word[i]) == containsEnglishAlpabet)
                {
                    if (symbolsCount.ContainsKey(word[i]))
                    {
                        symbolsCount[word[i]]++;//Прибавим значение к int по char.
                    }
                    else
                    {
                        symbolsCount[word[i]] = 1;//При первом нахождении делаем значение = 1.
                    }
                }
            }
            return symbolsCount;
        }
        
        public (bool, string) IsWordCorrect(string word)
        {
            Dictionary<char, int> unavailableSymbolsCount = GetSymbolsCountDictionary(word,false);

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
                message += "\n";
                message += string.Join(", ", symbolCounts);
                message += '.';
                return (false, message);
            }
            else
            {
                return (true, message);
            }


        }

        private string FindLongestVowelSubstring(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }

            string longestSubstring = string.Empty;

            for (int start = 0; start < word.Length; start++)
            {
                for (int end = start; end < word.Length; end++)
                {
                    string substring = word.Substring(start, end - start + 1);

                    if (IsVowel(substring.FirstOrDefault()) && IsVowel(substring.LastOrDefault()))
                    {
                        if (substring.Length > longestSubstring.Length)
                        {
                            longestSubstring = substring;
                        }
                    }
                }
            }
            return longestSubstring;
        }
        private bool IsVowel(char c)
        {
            return vowelLetters.Contains(c);
        }

    }
}
