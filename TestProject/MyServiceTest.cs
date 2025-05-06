using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeWebApp.Services;
namespace TestProject
{
    [TestFixture]
    public class MyServiceTest
    {
        [Test]
        public void Add_Correct_Word_Return_True()
        {
            TextService textService = new TextService();
            string word = "wordtrue";
            bool result = textService.IsWordCorrect(word).Item1;
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Add_Incorrect_Word_Return_False()
        {
            TextService textService = new TextService();
            string word = "wordt123rue";
            bool result = textService.IsWordCorrect(word).Item1;
            Assert.AreEqual(false, result);
        }

        //Тест задания 1:
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

        //Тест задания 2:
        [Test]
        public void Check_Task_2() 
        {
            bool result = false;
            TextService textService = new TextService();
            Dictionary<char, int> unavailableSymbolsCount = textService.GetSymbolsCountDictionaryFromEngAlphabet("Елочка", true);
            if(unavailableSymbolsCount.Count > 0) 
            {                
                result = false;
            }
            else 
            {
                result = true;
            }
            Assert.Equals(false, result);
            unavailableSymbolsCount = textService.GetSymbolsCountDictionaryFromEngAlphabet("tree", true);
        }
    }
}
