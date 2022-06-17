using Xamarin.Forms;
using YoungDevelopers.Services;

namespace YoungDevelopers
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new LoginPage());
            MainPage = new MainPage();
            //MainPage = new NavigationPage(new EditUserProfilePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
