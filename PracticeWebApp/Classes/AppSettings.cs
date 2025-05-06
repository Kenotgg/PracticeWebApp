using PracticeWebApp.Services.Interfaces;
namespace PracticeWebApp.Classes
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string[] Blacklist => _configuration.GetSection("Settings:Blacklist").Get<string[]>() ?? Array.Empty<string>();
        public int ParallelLimit => _configuration.GetValue<int>("Settings:ParallelLimit");
    }
}
