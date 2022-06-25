using DogsCompanion.Api.Client;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using YoungDevelopers.Client;
using YoungDevelopers.Services;

namespace YoungDevelopers
{
    public partial class App : Application
    {

        public App()
        {
            CreateClient();
            DataControl.LoadData();
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }

        private void CreateClient()
        {
            var httpClient = new HttpClient();
            App.Current.Properties["httpClient"] = httpClient;

            IToken tokenController = new TokenController();
            App.Current.Properties["tokenController"] = new TokenController();

            string baseUrl = "https://exp-webapi.herokuapp.com/";
            var dogsCompanionClient = new DogsCompanionClient(baseUrl, httpClient, tokenController);
            dogsCompanionClient.UpdatedToken += UpdateTokens; // Событие по получению новых токенов
            dogsCompanionClient.FailedServerUpdateToken += FailedServerUpdateToken;

            App.Current.Properties["dogsCompanionClient"] = dogsCompanionClient;
        }

        private async void UpdateTokens(string refreshToken, string accessToken)
        {
            var tokenController = (TokenController)App.Current.Properties["tokenController"];

            await tokenController.SetRefreshTokenAsync(refreshToken);
            await tokenController.SetAccessTokenAsync(accessToken);
        }

        private async void FailedServerUpdateToken()
        {
            var tokenController = (TokenController)App.Current.Properties["tokenController"];
            var refreshTokenRequest = new RefreshTokenRequest()
            {
                RefreshToken = await tokenController.GetRefreshTokenAsync(),
                AccessToken = await tokenController.GetAccessTokenAsync(),
            };

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            DataControl.SerializeToFile();
        }

        protected override void OnResume()
        {
        }
    }
}
