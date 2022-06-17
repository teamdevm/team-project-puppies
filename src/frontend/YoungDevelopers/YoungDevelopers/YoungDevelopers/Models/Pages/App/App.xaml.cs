using DogsCompanion.Api.Client;
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
            var httpClient = new HttpClient();
            App.Current.Properties["httpClient"] = httpClient;

            IToken tokenController = new TokenController();
            App.Current.Properties["tokenController"] = new TokenController();

            string baseUrl = "https://exp-webapi.herokuapp.com/";
            var dogsCompanionClient = new DogsCompanionClient(baseUrl, httpClient, tokenController);
            dogsCompanionClient.UpdatedToken += UpdateTokens; // Событие по получению новых токенов
            dogsCompanionClient.FailedServerUpdateToken += FailedUpdateToken; // Событие при неудачном обновлении токена
            dogsCompanionClient.FailedServerUpdateToken += FailedServerUpdateToken;

            App.Current.Properties["dogsCompanionClient"] = dogsCompanionClient;

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new LoginPage());
            MainPage = new MainPage();
            //MainPage = new NavigationPage(new EditUserProfilePage());
        }

        private async void UpdateTokens(string refreshToken, string accessToken)
        {
            var tokenController = (TokenController)App.Current.Properties["tokenController"];

            await tokenController.SetRefreshTokenAsync(refreshToken);
            await tokenController.SetAccessTokenAsync(accessToken);
        }

        private void FailedUpdateToken()
        {
            // TODO Выйти на экран авторизации, при неудачном обновлении токена
        }

        private async void FailedServerUpdateToken()
        {
            // TODO обработка события, если серверная ошибка обновления токена
            // По уму сделать крутилку, что идет ожидание и пытаться повторно обновить токен

            var tokenController = (TokenController)App.Current.Properties["tokenController"];
            var refreshTokenRequest = new RefreshTokenRequest()
            {
                RefreshToken = await tokenController.GetRefreshTokenAsync(),
                AccessToken = await tokenController.GetAccessTokenAsync(),
            };

            // Повторное обновление токена
            /*
            try
            {
                var dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];
                await dogsCompanionClient.RefreshAsync(refreshTokenRequest);
            }
            catch (System.Exception)
            {
                // TODO обработка ошибки
                throw;
            }
            */

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
