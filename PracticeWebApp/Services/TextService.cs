using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services.Interfaces;
using System.Linq;
using System.Text;
using PracticeWebApp.Classes;
using PracticeWebApp.Controllers;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
namespace PracticeWebApp.Services
{
    public class TextService : ITextService
    {
        char[] englishAlphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] vowelLetters = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<TextController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBlackListSettings _blackListSettings;
        public TextService(ILogger<TextController> logger, IHttpClientFactory clientFactory, IConfiguration configuration, IBlackListSettings blackListSettings) 
        {                  
            _clientFactory = clientFactory;
            _logger = logger;
            _configuration = configuration;
            _blackListSettings = blackListSettings;
        }
        public async Task<string> ReturnProcessedString(string word, string sortAlgorithm)
        {
            bool isCorrect = IsWordCorrect(word).Item1;
            if (!isCorrect)
            {
                return "Самая длинная подстрока: " + FindLongestVowelSubstring(word.ToString()) + ", " + IsWordCorrect(word).Item2;
            }

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
            foreach (var pair in GetSymbolsCountDictionary(processedWord, true))
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
            if (FindLongestVowelSubstring(processedWord.ToString()).Count() > 0)
            {
                resultMessage.Append("\n");
                resultMessage.Append("\n");
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
            if(sortAlgorithm == "treeSort") 
            {
                TreeSortAlgorithm treeAlgorithm = new TreeSortAlgorithm();
                resultMessage.Append("Результат сортировки алгоритмом 'treeSort':");
                resultMessage.Append("\n");
                resultMessage.Append(treeAlgorithm.Sort(processedWord));
                resultMessage.Append(".");
            }
            resultMessage.Append("\n");
            resultMessage.Append("\n");
            int? randomNumber = await GetRandomNumberFromApi(0, processedWord.Length - 1);
            if (randomNumber != null) 
            {
                resultMessage.Append("Урезанное слово через 'api': ");
                resultMessage.Append("\n");
                resultMessage.Append(RemoveSymbolByNumber(processedWord, randomNumber.Value));
                resultMessage.Append('.');
            }
            else 
            {
                Random random = new Random();
                int randomNumberNet = random.Next(0, processedWord.Length - 1);
                resultMessage.Append("Урезанное слово через '.net': ");
                resultMessage.Append("\n");
                resultMessage.Append(RemoveSymbolByNumber(processedWord, randomNumberNet));
                resultMessage.Append('.');
            }
            
            
            return resultMessage.ToString();
        }

        
        public string RemoveSymbolByNumber(string word,int index) 
        {
            List<char> charredWord = word.ToCharArray().ToList();
            charredWord.RemoveAt(index);
            return new string(charredWord.ToArray());
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
        public async Task<int?> GetRandomNumberFromApi(int min, int max) 
        {
            var client = _clientFactory.CreateClient();
            string baseUrl = _configuration["RandomApi"];
            string apiUrl = $"{baseUrl}?min={min}&max={max}&count=1";
            try
            {
                if(min < 0) 
                {
                    throw new Exception("Нельзя обратиться к отрицательному индексу.");
                }
                var response = await client.GetAsync(apiUrl);
                // Проверка успешности запроса
                if (response.IsSuccessStatusCode)
                {
                    // Чтение содержимого ответа как строки
                    var jsonString = await response.Content.ReadAsStringAsync();
                    try 
                    {
                        var numbers = JsonSerializer.Deserialize<int[]>(jsonString);
                        if (numbers != null && numbers.Length > 0)
                        {
                            int randomNumber = numbers[0];
                            _logger.LogInformation($"Successfully parsed random number: {randomNumber}");
                            return randomNumber;
                        }
                        else
                        {
                            _logger.LogWarning("API returned an empty array.");
                            return null;
                        }
                        
                    }
                    catch(JsonException ex) 
                    {
                        _logger.LogError($"Error deserializing JSON: {ex.Message}, JSON: {jsonString}");
                        return null;
                    }
                    
                }
                else
                {
                    _logger.LogError($"API request failed with status code {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                // Обработка исключений, связанных с запросом
                _logger.LogWarning($"HTTP request error: {ex.Message}");
                return null;
            }
        }
        public (bool, string) IsWordCorrect(string word)
        {
            // Проверяем, находится ли слово в черном списке (игнорируя регистр и пробелы)
            if (_blackListSettings.Words.Any(blacklistedWord => string.Equals(blacklistedWord.Trim(), word.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, $"Word '{word}' is blacklisted.");
            }

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
