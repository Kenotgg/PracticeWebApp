namespace PracticeWebApp.Services.Interfaces
{
    public interface ITextService
    {
         Task<string> ReturnProcessedString(string word, string sortType);
         public (bool, string) IsWordCorrect(string word);
    }
}
