using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        StackLayout layout, lay_registraion;
        Image im_pug;
        Label lb_dogass, lb_register;
        ControlEntry en_login, en_password;
        Button bt_login, bt_register, bt_recover;
        public LoginPage()
        {
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.LightGray;

            // Мопс
            im_pug  = new Image 
            { 
                Source = "pug_yawn.jpg", 
                Margin = new Thickness(50, 50, 50, 0),
                BackgroundColor = Color.LightGray,
            };

            layout.Children.Add(im_pug);

            //Надпись Dog Assistant
            lb_dogass = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Dog Assistant",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                Margin = new Thickness(50, 5, 50, 50),
            };

            layout.Children.Add(lb_dogass);

            // Поле Email с границами
            en_login = new ControlEntry
            {
                Placeholder = "E-Mail",
                FontFamily = "Cascadia Code Light",
                Keyboard = Keyboard.Email,
                Margin = new Thickness(0,-15,0,-17.5),
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            Frame fr_login = new Frame
            {
                CornerRadius = 10,
                IsClippedToBounds = true,
                HeightRequest = 10,
                Margin = new Thickness(10,0,10,0),
            };


            layout.Children.Add(fr_login);

            // Поле Пароль с границами
            en_password = new ControlEntry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontFamily = "Cascadia Code Light",
                BackgroundColor = Color.White,
                Margin = new Thickness(0, -15, 0, -17.5),
            };

            Frame fr_pass = new Frame
            {
                Content = en_password,
                CornerRadius = 10,
                HeightRequest = 10,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 0),
            };

            layout.Children.Add(fr_pass);

            // Кнопка "Вход" с фоном
            bt_login = new Button
            {
                Text = "Войти",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Turquoise,
                CornerRadius = 10,
                WidthRequest = 370
            };
            bt_login.Clicked += OnLoginButtonClicked;

            layout.Children.Add(bt_login);

            // Кнопка забыл пароль
            bt_recover = new Button
            {
                Text = "Забыли пароль?",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                BackgroundColor = Color.Transparent,
                TextColor = Color.Gray,
            };
            bt_recover.Clicked += OnForgetButtonClicked;
            layout.Children.Add(bt_recover);

            // Регистрация
            lay_registraion = new StackLayout();
            lay_registraion.Orientation = StackOrientation.Horizontal;
            lay_registraion.HorizontalOptions = LayoutOptions.Center;
            lay_registraion.Margin = new Thickness(0, 180, 0, 0);


            // Label нет аккаунта
            lb_register = new Label
            {
                FontFamily = "Cascadia Code Light",
                Text = "Нет аккаунта?",
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
            };
            lay_registraion.Children.Add(lb_register);

            // Кнопка "Регистрация" с фоном
            bt_register = new Button
            {
                FontFamily = "Cascadia Code Light",
                Text = "Зарегистрироваться",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Turquoise,
                CornerRadius = 10,
            };
            bt_register.Clicked += OnRegisterButtonClicked;

            lay_registraion.Children.Add(bt_register);

            layout.Children.Add(lay_registraion);



            this.Content = layout;
                      

            InitializeComponent();
            fr_login.Content = en_login;
        }

        // --------------- Обработка событий --------------- //
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Обращение к Васе
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new RegistrationPage());
        }

        private async void OnForgetButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgetPassPage());
        }
    }
}