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

            #region Инициализация свойства для хранения данных

            // Загрузка данных данных
            /* 1) При открытии приложения загрузить новые данные с сервера
             * 2) Если загрузить не удалось - достаем из сериализованного файла в Properties
             * 3) При переходе на страницы, которые отображают/запрашивают данные, обращаемся к серверу и обновляем Properties
             * 4) По закрытии приложения сериализовать Properties данные снова в файл
            */
            #endregion
            //((Data)App.Current.Properties["storedata"]).Dogs.Where(s => s.Id == 1).First().Name = "Ебырь";

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new EditUserProfilePage());
            //App.Current.Properties["currentuserid"] = 0;
            //MainPage = new GroomingInfoPage(0);
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
            dogsCompanionClient.FailedServerUpdateToken += FailedUpdateToken; // Событие при неудачном обновлении токена
            dogsCompanionClient.FailedServerUpdateToken += FailedServerUpdateToken;

            App.Current.Properties["dogsCompanionClient"] = dogsCompanionClient;
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
            DataControl.SerializeToFile();
        }

        protected override void OnResume()
        {
        }
    }
}
