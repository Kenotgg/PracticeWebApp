namespace PracticeWebApp.Services.Interfaces
{
    public interface IAppSettings
    {
        string[] Blacklist { get; }
        int ParallelLimit { get; }
    }
}
