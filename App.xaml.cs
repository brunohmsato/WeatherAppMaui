namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver == true)
            {
                MainPage = new WelcomePage();
            }
            else
            {
                MainPage = new WeatherPage();
            }

        }
    }
}
