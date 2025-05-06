using PracticeWebApp.Services.Interfaces;

namespace PracticeWebApp.Classes
{
    public class BlackListSettings : IBlackListSettings
    {
        private readonly IConfiguration _configuration;
        public BlackListSettings(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string[] Words => _configuration.GetSection("Settings:Blacklist").Get<string[]>();
    }
}
