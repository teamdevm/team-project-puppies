using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcRegPage : ContentPage
    {
        #region Инициализация 
        private StackLayout layout;
        private Button bt_sing_vet, bt_sign_groom;

        #endregion
        public ProcRegPage()
        {
            layout = new StackLayout();

            #region Элементы страницы

            bt_sing_vet = new Button()
            {
                Text = "Записаться в ветклинику",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(105,233,165),
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 20, 0, 10),
            };

            layout.Children.Add(bt_sing_vet);
            bt_sing_vet.Clicked += OnSingVetPressed;

            bt_sign_groom = new Button()
            {
                Text = "Записаться в груминг-салон",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(105,233,165),
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_sign_groom);
            bt_sign_groom.Clicked += OnSingGroomPressed;

            #endregion

            this.Content = layout;
            InitializeComponent();
        }

        #region Обработка событий

        public async void OnSingVetPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VetClinicsPage());
        }

        public async void OnSingGroomPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomingsPage());
        }

        #endregion
    }
}