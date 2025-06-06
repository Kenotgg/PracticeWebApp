using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeWebApp.Services;
using PracticeWebApp.Classes;
namespace TestProject
{
    [TestFixture]
    public class MyServiceTest
    {
        //Тест задания 1:
        //Тестирование поставленных примеров.
        [Test]
        public void Check_Transform_Word_By_Default_Examples()
        {
            TextService textService = new TextService();
            //1
            string word1 = "a";
            string res1 = textService.TransformWord(word1);
            Assert.AreEqual("aa", res1);
            //2
            string word2 = "abcdef";
            string res2 = textService.TransformWord(word2);
            Assert.AreEqual("cbafed", res2);
            //3
            string word3 = "abcde";
            string res3 = textService.TransformWord(word3);
            Assert.AreEqual("edcbaabcde", res3);
        }

        //Проверка при вводе корректного слова.
        [Test]
        public void Add_Correct_Word_Return_True()
        {
            TextService textService = new TextService();
            string word = "wordtrue";
            bool result = textService.IsWordCorrect(word).Item1;
            Assert.AreEqual(true, result);
        }

        //Проверка при вводе некорректного слова.
        [Test]
        public void Add_Incorrect_Word_Return_False()
        {
            TextService textService = new TextService();
            string word = "wordt123rue";
            bool result = textService.IsWordCorrect(word).Item1;
            Assert.AreEqual(false, result);
        }

        //Тест задания 2: 
        //Проверка на вход исключительно английских символов в нижнем регистре.

        [Test]
        public void Check_Is_English_Alphabet_Matches_Correct()
        {
            TextService textService = new TextService();
            //1
            bool res1 = textService.IsWordCorrect("Елочка").Item1;
            Assert.AreEqual(false, res1);
            //2
            bool res2 = textService.IsWordCorrect("tree").Item1;
            Assert.AreEqual(true, res2);
            //3
            bool res3 = textService.IsWordCorrect("tree3").Item1;
            Assert.AreEqual(false, res3);
            //4
            bool res4 = textService.IsWordCorrect("      ").Item1;
            Assert.AreEqual(false, res4);
            //5
            bool res5 = textService.IsWordCorrect("correct").Item1;
            Assert.AreEqual(true, res5);
            //6
            bool res6 = textService.IsWordCorrect("CAPSLOCK").Item1;
            Assert.AreEqual(false, res6);
        }
        [Test]
        public void Check_Is_Output_Correct()
        {
            TextService textService = new TextService();
            string word = "Елочка";
            string expectedMessage = "Были введены некорректные символы: \nЕ - 1 раз, л - 1 раз, о - 1 раз, ч - 1 раз, к - 1 раз, а - 1 раз."; // Ожидаемое сообщение

            (bool isCorrect, string actualMessage) result = textService.IsWordCorrect(word);

            Assert.IsFalse(result.isCorrect); // Проверяем, что слово некорректно
            Assert.AreEqual(expectedMessage, result.actualMessage); // Проверяем, что сообщение об ошибке совпадает с ожидаемым
        }

        //Тест задания 3:
        //Проверяем совпадает ли количество повторений символов.
        [Test]
        public void Check_Is_Symbols_Count_Correct()
        {
            TextService textService = new TextService();
            string word = "fffddaaaa";
            bool countEnglishSymbols = true; // Указываем, что функция должна считать английские символы
            Dictionary<char, int> symbolsCount = textService.GetSymbolsCountDictionaryByLanguage(word, countEnglishSymbols);
            // Проверяем, что словарь содержит ожидаемые символы и их количество
            Assert.AreEqual(3, symbolsCount['f']);
            Assert.AreEqual(2, symbolsCount['d']);
            Assert.AreEqual(4, symbolsCount['a']);
            // Дополнительная проверка: убедимся, что в словаре нет лишних символов
            Assert.AreEqual(3, symbolsCount.Count); // Всего 3 уникальных символа
        }

        //Тест задания 4: 
        //Нахождение самой длинной подстроки начинающейся на гласную букву.
        [Test]
        public void Check_Is_Vowel_Substring_Correct()
        {
            TextService textService = new TextService();
            //1
            string word1 = "baeoiyb";
            string result1 = textService.FindLongestVowelSubstring(textService.TransformWord(word1));
            Assert.AreEqual("yioeabbaeoiy", result1);
            //2
            string word2 = "abcdeiouxyzaeiou";
            string result2 = textService.FindLongestVowelSubstring(textService.TransformWord(word2));
            Assert.AreEqual("uoiedcbauoieazy", result2); // Должна вернуть "eiouxy" а не "aeiou"
            //3
            string word3 = "aeioubcdefgh";
            string result3 = textService.FindLongestVowelSubstring(textService.TransformWord(word3));
            Assert.AreEqual("uoieahgfe", result3);
        }

        [Test]
        public void Check_Is_Vowel_Substring_Correct_By_Default_Examples()
        {
            TextService textService = new TextService();
            //1
            string word1 = "a";
            string result1 = textService.FindLongestVowelSubstring(textService.TransformWord(word1));
            Assert.AreEqual("aa", result1);
            //2
            string word2 = "abcdef";
            string result2 = textService.FindLongestVowelSubstring(textService.TransformWord(word2));
            Assert.AreEqual("afe", result2);
            //3
            string word3 = "abcde";
            string result3 = textService.FindLongestVowelSubstring(textService.TransformWord(word3));
            Assert.AreEqual("edcbaabcde", result3);
        }


        //Тест задания 5: 
        //Проверка правильной работы быстрой сортировки (Quicksort) и сортировки деревом (Tree sort).
        [Test]
        public void QuickSort_Test()
        {
            QuickSortAlgorithm quickSortAlgorithm = new QuickSortAlgorithm();
            //1
            string word1 = "abcdfge";
            string res1 = quickSortAlgorithm.Sort(word1);
            Assert.AreEqual("abcdefg", res1);
            //2
            string word2 = "aaaaaaaaaaaaaaaaaaabcddcfdsffjopiofuoidsuofdfggggggggggggeeeeeee";
            string res2 = quickSortAlgorithm.Sort(word2);
            Assert.AreEqual("aaaaaaaaaaaaaaaaaaabccdddddeeeeeeeffffffggggggggggggiijoooopssuu", res2);
            //3
            string word3 = "ajieofhtyobzifaldurhvucixydz";
            string res3 = quickSortAlgorithm.Sort(word3);
            Assert.AreEqual("aabcddeffhhiiijloortuuvxyyzz", res3);
        }


        [Test]
        public void TreeSort_Test()
        {
            TreeSortAlgorithm treeSortAlgorithm = new TreeSortAlgorithm();
            //1
            string word1 = "abcdfge";
            string res1 = treeSortAlgorithm.Sort(word1);
            Assert.AreEqual("abcdefg", res1);
            //2
            string word2 = "aaaaaaaaaaaaaaaaaaabcddcfdsffjopiofuoidsuofdfggggggggggggeeeeeee";
            string res2 = treeSortAlgorithm.Sort(word2);
            Assert.AreEqual("aaaaaaaaaaaaaaaaaaabccdddddeeeeeeeffffffggggggggggggiijoooopssuu", res2);
            //3
            string word3 = "ajieofhtyobzifaldurhvucixydz";
            string res3 = treeSortAlgorithm.Sort(word3);
            Assert.AreEqual("aabcddeffhhiiijloortuuvxyyzz", res3);
        }
    }
}
