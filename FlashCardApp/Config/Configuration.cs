using System.Configuration;

namespace FlashCardApp.Config;

public class Configuration
{
    public string ConnectionString
    {
        get
        {
            return ConfigurationManager.AppSettings.Get("connectionString")
                ?? throw new NullReferenceException("connection string not provided");
        }
    }
}
