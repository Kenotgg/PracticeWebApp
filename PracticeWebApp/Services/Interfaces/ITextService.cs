namespace PracticeWebApp.Services.Interfaces
{
    public interface ITextService
    {
         Task<string> ReturnProcessedString(string word);
         public (bool, string) IsWordCorrect(string word);
    }
}
