namespace Portfolio_AppRepo_UI.Classes
{
    public class Settings
    {
        public static string BaseUrl { get; set; }
        public static string FileBaseUrl { get; set; }
        public static string SharepointPassword { get; set; }
        public static string SharepointUsername { get; set; }
        public static string CurrentUrl { get; set; }
        public static string DownloadHostUrl { get; set; }

        static Settings()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            BaseUrl = root.GetValue<string>("Endpoints:BaseUrl");
            FileBaseUrl = root.GetValue<string>("Endpoints:FileBaseUrl");
            SharepointPassword = root.GetValue<string>("Endpoints:SharepointPassword");
            SharepointUsername = root.GetValue<string>("Endpoints:SharepointUsername");
            CurrentUrl = root.GetValue<string>("Endpoints:CurrentUrl");
            DownloadHostUrl = root.GetValue<string>("Endpoints:DownloadHostUrl");
        }
    }
}
