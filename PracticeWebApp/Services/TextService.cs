using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services.Interfaces;
using System.Linq;
using System.Text;
using PracticeWebApp.Classes;
namespace PracticeWebApp.Services
{
    public class TextService : ITextService
    {
        char[] englishAlphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] vowelLetters = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        public async Task<string> ReturnProcessedString(string word, string sortAlgorithm)
        {
            bool isCorrect = IsWordCorrect(word).Item1;
            if (!isCorrect)
            {
                return "Самая длинная подстрока: " + FindLongestVowelSubstring(word.ToString()) + ", " + IsWordCorrect(word).Item2;
            }
            string processedWord = TransformWord(word);
            return FormatResult(processedWord, sortAlgorithm);
        }

        private string FormatResult(string processedWord, string sortAlgorithm) 
        {
            StringBuilder resultMessage = new StringBuilder();
            resultMessage.Append("Обработанная строка: ");
            resultMessage.Append("\n");
            resultMessage.Append(processedWord + ".");
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            resultMessage.Append("Cколько повторений символов встречается: ");
            resultMessage.Append("\n");
            resultMessage.Append(GetSymbolCountsAsString(processedWord));
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            if (FindLongestVowelSubstring(processedWord.ToString()).Count() > 0)
            {
                resultMessage.Append("Cамая длинная подстрока: ");
                resultMessage.Append("\n");
                resultMessage.Append(FindLongestVowelSubstring(processedWord.ToString()) + ".");
            }
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            if (sortAlgorithm == "quickSort" || sortAlgorithm == null)
            {
                QuickSortAlgorithm quickSortAlgorithm = new QuickSortAlgorithm();
                resultMessage.Append("Результат сортировки алгоритмом 'quickSort':");
                resultMessage.Append("\n");
                resultMessage.Append(quickSortAlgorithm.Sort(processedWord));
                resultMessage.Append(".");
            }
            if (sortAlgorithm == "treeSort")
            {
                TreeSortAlgorithm treeAlgorithm = new TreeSortAlgorithm();
                resultMessage.Append("Результат сортировки алгоритмом 'treeSort':");
                resultMessage.Append("\n");
                resultMessage.Append(treeAlgorithm.Sort(processedWord));
                resultMessage.Append(".");
            }
            return resultMessage.ToString();
        }

        private string GetSymbolCountsAsString(string word) 
        {
            List<string> symbolCounts = new List<string>();
            foreach (var pair in GetSymbolsCountDictionaryByLanguage(word, true))
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
            return string.Join(", ", symbolCounts) + ".";
        }

        public string TransformWord(string word) 
        {
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
            return processedWord;
        }

        public Dictionary<char,int> GetSymbolsCountDictionaryByLanguage(string word, bool checkEnglishSymbols) 
        {
            Dictionary<char, int> symbolsCount = new Dictionary<char, int>();
            if (checkEnglishSymbols) 
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (englishAlphabet.Contains(word[i]) == true)
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
            else 
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (englishAlphabet.Contains(word[i]) == false)
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
           
        }
        
        public (bool, string) IsWordCorrect(string word)
        {
            Dictionary<char, int> unavailableSymbolsCount = GetSymbolsCountDictionaryByLanguage(word, false);

            if (unavailableSymbolsCount.Count > 0)
            {
                string message = GenerateErrorMessage(unavailableSymbolsCount);
                return (false, message);
            }
            else
            {
                return (true, string.Empty); 
            }
        }

        private string GenerateErrorMessage(Dictionary<char, int> unavailableSymbolsCount)
        {
            string message = "Были введены некорректные символы: \n";
            List<string> symbolCounts = new List<string>();
            foreach (var pair in unavailableSymbolsCount)
            {
                string symbolDescription = GetSymbolDescription(pair.Key, pair.Value);
                symbolCounts.Add(symbolDescription);
            }
            message += string.Join(", ", symbolCounts) + ".";
            return message;
        }

        public string GetSymbolDescription(char symbol, int count) 
        {
            switch (symbol)
            {
                case ' ':
                    return $"пробел - {count} раз";
                case ',':
                    return $"запятая - {count} раз";
                case '.':
                    return $"точка - {count} раз";
                default:
                    return $"{symbol} - {count} раз";
            }
        }

        public string FindLongestVowelSubstring(string word)
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
