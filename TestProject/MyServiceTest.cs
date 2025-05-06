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
        //Тестирование поставленных примеров
        [Test]
        public void Check_Task_1()
        {
            TextService textService = new TextService();
            string word1 = "a";
            string res1 = textService.TransformWord(word1);
            Assert.AreEqual("aa", res1);

            string word2 = "abcdef";
            string res2 = textService.TransformWord(word2);
            Assert.AreEqual("cbafed", res2);

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
        //Проверка на вход исключительно английских символов в нижнем регистре
        [Test]
        public void Check_Task_2() 
        {
            TextService textService = new TextService();
            bool res1 = textService.IsWordCorrect("Елочка").Item1;
            Assert.AreEqual(false, res1);

            bool res2 = textService.IsWordCorrect("tree").Item1;
            Assert.AreEqual(true, res2);

            bool res3 = textService.IsWordCorrect("tree3").Item1;
            Assert.AreEqual(false, res3);

            bool res4 = textService.IsWordCorrect("      ").Item1;
            Assert.AreEqual(false, res4);

            bool res5 = textService.IsWordCorrect("correct").Item1;
            Assert.AreEqual(true, res5);

            bool res6 = textService.IsWordCorrect("CAPSLOCK").Item1;
            Assert.AreEqual(false, res6);

        }


        //Тест задания 3:
        //Проверяем совпадает ли количество повторений символов
        [Test]
        public void Check_Task_3()
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
        public void Check_Task_4() 
        {
            TextService textService = new TextService();
            string word0 = "baeoiyb";
            string result0 = textService.FindLongestVowelSubstring(word0);
            Assert.AreEqual("aeoiy",result0);
            
            string word1 = "abcdeiouxyzaeiou";
            string result1 = textService.FindLongestVowelSubstring(word1);
            Assert.AreEqual("abcdeiouxyzaeiou", result1); // Должна вернуть "eiouxy" а не "aeiou"
            
            string word2 = "aeioubcdefgh";
            string result2 = textService.FindLongestVowelSubstring(word2);
            Assert.AreEqual("aeioubcde", result2);
        }


        //Тест задания 5: 
        //Проверка правильной работы быстрой сортировки и сортировки деревом
        [Test]
        public void Quick_Sort_Test()
        {
            QuickSortAlgorithm quickSortAlgorithm = new QuickSortAlgorithm();
            string word0 = "abcdfge";
            string res0 = quickSortAlgorithm.Sort(word0);
            Assert.AreEqual("abcdefg", res0);

            string word1 = "aaaaaaaaaaaaaaaaaaabcddcfdsffjopiofuoidsuofdfggggggggggggeeeeeee";
            string res1 = quickSortAlgorithm.Sort(word1);
            Assert.AreEqual("aaaaaaaaaaaaaaaaaaabccdddddeeeeeeeffffffggggggggggggiijoooopssuu", res1);

            string word2 = "ajieofhtyobzifaldurhvucixydz";
            string res2 = quickSortAlgorithm.Sort(word2);
            Assert.AreEqual("aabcddeffhhiiijloortuuvxyyzz", res2);
        }


        [Test]
        public void Tree_Sort_Test()
        {
            TreeSortAlgorithm treeSortAlgorithm = new TreeSortAlgorithm();
            string word0 = "abcdfge";
            string res0 = treeSortAlgorithm.Sort(word0);
            Assert.AreEqual("abcdefg", res0);

            string word1 = "aaaaaaaaaaaaaaaaaaabcddcfdsffjopiofuoidsuofdfggggggggggggeeeeeee";
            string res1 = treeSortAlgorithm.Sort(word1);
            Assert.AreEqual("aaaaaaaaaaaaaaaaaaabccdddddeeeeeeeffffffggggggggggggiijoooopssuu", res1);

            string word2 = "ajieofhtyobzifaldurhvucixydz";
            string res2 = treeSortAlgorithm.Sort(word2);
            Assert.AreEqual("aabcddeffhhiiijloortuuvxyyzz", res2);
        }
    }
}
