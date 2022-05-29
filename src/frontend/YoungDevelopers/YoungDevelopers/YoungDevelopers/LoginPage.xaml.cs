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
        public LoginPage()
        {
            var layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;

            // Мопс
            Image im_pug  = new Image { Source = "pug_yawn.jpg", Margin = new Thickness(50, 50, 50, 0)};

            layout.Children.Add(im_pug);

            //Надпись Dog Assistant
            Label lb_dogass = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Dog Assistant",
                Margin = new Thickness(50, 5,50,50),
            };

            layout.Children.Add(lb_dogass);

            // Поле Email с границами
            var en_login = new ControlEntry
            {
                Placeholder = "E-Mail",
                FontFamily = "Cascadia Code Light",
                HeightRequest = 50,
                Keyboard = Keyboard.Email,
                Margin = new Thickness(10, 10),
                MyTintColor = Color.Purple,
                MyHighlightColor = Color.Black
            };

            layout.Children.Add(en_login);

            // Поле Пароль с границами
            var en_password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontFamily = "Cascadia Code Light",
                HeightRequest = 50,
                Margin = new Thickness(10, 10)
            };

            layout.Children.Add(en_password);

            // Кнопка "Вход" с фоном
            Button bt_login = new Button
            {
                Text = "Log In",
                HorizontalOptions = LayoutOptions.Center,
            };
            bt_login.Clicked += OnLoginButtonClicked;

            layout.Children.Add(bt_login);

            //Нет аккаунт Label
            //Надпись Dog Assistant
            Label lb_noaccount = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Нет аккаунта?",
                Margin = new Thickness(50, 5, 50, 5),
            };
            layout.Children.Add(lb_noaccount);

            // Кнопка "Регистрация" с фоном
            Button bt_register = new Button
            {
                Text = "Sign Up",
                HorizontalOptions = LayoutOptions.Center,
            };
            bt_register.Clicked += OnLoginButtonClicked;

            layout.Children.Add(bt_register);

            // Забыл Пароль без фона

            this.Content = layout;

            

            InitializeComponent();
            
        }

        // --------------- Обработка событий ---------------
        private void OnLoginButtonClicked(object sender, System.EventArgs e)
        {

        }

        private void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {

        }
    }
}