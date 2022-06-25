﻿using DogsCompanion.Api.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rox;
using System.Net.Http;
using YoungDevelopers.Client;

namespace YoungDevelopers
{
    public class SendAuth
    {
        public string email = "";
        public string password = "";
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region Инициализация 
        private StackLayout layout, lay_registraion;
        private Image im_pug;
        private Label lb_dogass, lb_register,
            lb_login_error, lb_pair_error, lb_logging_er;
        private ControlEntry en_login, en_password;
        private Button bt_login, bt_register, bt_recover;
        private Frame fr_login, fr_pass;
        private VideoView video;
        private Action startVideo;
        private Regex re_email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private SendAuth sendauth;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private HttpClient httpClient = DataControl.httpClient;
        private TokenController tokenController = DataControl.tokenController;

        #endregion
        public LoginPage()
        {
            string Message = DataControl.LoadData();
            if (Message != "")
            {
                ErrorAlert(Message);
            }

            this.Title = "Авторизация";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Получение данных

            #endregion

            #region Элементы страницы
            // Мопс
            im_pug = new Image
            {
                Source = "ben.jpg",
                Margin = new Thickness(50, 50, 50, 0),
                BackgroundColor = Color.FromRgb(242, 242, 242),
            };

            layout.Children.Add(im_pug);

            //video = new VideoView
            //{
            //    Source = "ben.mp4",
            //    ShowController = true,
            //    Margin = new Thickness(50, 50, 50, 0),
            //};
            //layout.Children.Add(video);

            //Надпись Dog Assistant
            lb_dogass = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Dog's Companion",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
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

            en_login.TextChanged += OnLoginTextChanged;
            en_login.Unfocused += OnLoginUnfocused;
            en_login.Focused += OnLoginFocused;

            fr_login = new Frame
            {
                Content = en_login,
                CornerRadius = 10,
                BackgroundColor = Color.White,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                HeightRequest = 10,
                Margin = new Thickness(10,0,10,0),
                HasShadow = true,
            };


            layout.Children.Add(fr_login);

            // Ошибка ввода логина
            lb_login_error = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат e-mail",
                Margin = new Thickness(15, -3, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_login_error);

            // Поле Пароль с границами
            en_password = new ControlEntry
            {
                Placeholder = "Пароль",
                IsPassword = true,
                FontFamily = "Cascadia Code Light",
                BackgroundColor = Color.White,
                Margin = new Thickness(0, -15, 0, -17.5),
            };

            en_password.Focused += OnPasswordFocused;
            en_password.Unfocused += OnPasswordUnFocused;

            fr_pass = new Frame
            {
                Content = en_password,
                CornerRadius = 10,
                HeightRequest = 10,
                IsClippedToBounds = true,
                BackgroundColor = Color.White,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow= true,
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
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370
            };
            bt_login.Clicked += OnLoginButtonClicked;

            layout.Children.Add(bt_login);

            // Несовпадение логина и пароля
            lb_pair_error = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Несовпадение логина и пароля",
                Margin = new Thickness(15, -3, 0, -16),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_pair_error);

            // Ошибка авторизации
            lb_logging_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "",
                Margin = new Thickness(15, -3, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_logging_er);

            // Кнопка забыл пароль
            bt_recover = new Button
            {
                Text = "Забыли пароль?",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, -5, 0, 0),
                TextColor = Color.Gray,
            };
            bt_recover.Clicked += OnForgetButtonClicked;
            layout.Children.Add(bt_recover);

            // Регистрация
            lay_registraion = new StackLayout();
            lay_registraion.Orientation = StackOrientation.Horizontal;
            lay_registraion.HorizontalOptions = LayoutOptions.Center;
            lay_registraion.VerticalOptions = LayoutOptions.End;
            lay_registraion.Padding = new Thickness(0, 150, 0, 15);


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
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
            };
            bt_register.Clicked += OnRegisterButtonClicked;

            lay_registraion.Children.Add(bt_register);

            layout.Children.Add(lay_registraion);

            #endregion

            this.Content = layout;
                      

            InitializeComponent();
        }

        #region Обработка событий
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (en_login.Text == null || en_password.Text == null)
            {
                return;
            }
            if (lb_login_error.IsVisible)
            {
                lay_registraion.Padding = new Thickness(0, 130, 0, 20);
            }
            else
            {
                lay_registraion.Padding = new Thickness(0, 150, 0, 15);
            }
            lb_logging_er.IsVisible = false;
            if (fr_login.BorderColor == Color.White)
            {
                sendauth = new SendAuth();
                sendauth.email = en_login.Text;
                sendauth.password = en_password.Text;

                try
                {
                    var authInfo = new AuthInfo()
                    {
                        Email = en_login.Text,
                        Password = en_password.Text,
                    };

                    var authResponse = await dogsCompanionClient.AuthenticateAsync(authInfo);

                    // Установление ключа в httpclient
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponse.AccessToken);

                    // Сохранение токенов в хранилище
                    await tokenController.SetRefreshTokenAsync(authResponse.RefreshToken);
                    await tokenController.SetAccessTokenAsync(authResponse.AccessToken);
                    ReadDog addDog = await dogsCompanionClient.GetDogsAsync();

                    // Загрузить информацию о пользователе
                    DataControl.SetAuthData(authResponse, addDog);

                    // Переход на главную страницу
                    // TODO сохранить полученные данные из authInfo
                    // TODO поменять Main страницу
                    App.Current.MainPage = new MainPage();
                }
                catch (ApiException apiExc)
                {
                    if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                    {
                        lay_registraion.Padding = new Thickness(0, 130, 0, 20);
                        lb_logging_er.Text = "Сервис недоступен";
                        lb_logging_er.IsVisible = true;
                    }
                    else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        lay_registraion.Padding = new Thickness(0, 130, 0, 20);
                        lb_logging_er.Text = "Неверный логин или пароль";
                        lb_logging_er.IsVisible = true;
                    }
                }
                catch (Exception)
                {
                    lay_registraion.Padding = new Thickness(0, 130, 0, 20);
                    lb_logging_er.Text = "Произошла непредвиденная ошибка";
                    lb_logging_er.IsVisible = true;
                }
            }            
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void OnForgetButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgetPassPage());
        }

        private void OnLoginTextChanged(object sender, EventArgs e)
        {
            fr_login.BorderColor = Color.White;
            lb_login_error.IsVisible = false;
            fr_login.BackgroundColor = Color.White;
            en_login.BackgroundColor = Color.White;
            en_login.TextColor = Color.Black;
            lay_registraion.Padding = new Thickness(0, 150, 0, 15);
        }

        private void OnLoginUnfocused(object sender, EventArgs e)
        {
            fr_login.BorderColor = Color.White;
            if (en_login.Text == "" || en_login.Text == null) return;
            else
            {
                Match match = re_email.Match(en_login.Text);
                if (!match.Success)
                {
                    fr_login.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_login_error.IsVisible = true;
                    fr_login.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_login.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_login.TextColor = Color.FromRgb(194, 85, 85);
                    lay_registraion.Padding = new Thickness(0, 130, 0, 20);
                }
                else
                {
                    fr_login.BorderColor = Color.White;
                    lb_login_error.IsVisible = false;
                    fr_login.BackgroundColor = Color.White;
                    en_login.BackgroundColor = Color.White;
                    en_login.TextColor = Color.Black;
                    lay_registraion.Padding = new Thickness(0, 150, 0, 15);
                    
                }
            }
        }
        private void OnLoginFocused(object sender, EventArgs e)
        { 
            lb_logging_er.IsVisible = false;
            fr_login.BorderColor = Color.SpringGreen;
        }

        private void OnPasswordFocused(object sender, EventArgs e)
        {
            if (lb_login_error.IsVisible)
            {
                lay_registraion.Padding = new Thickness(0, 130, 0, 20);
            }
            else
            {
                lay_registraion.Padding = new Thickness(0, 150, 0, 15);
            }
            lb_logging_er.IsVisible = false;
            fr_pass.BorderColor = Color.SpringGreen;
        }

        private void OnPasswordUnFocused(object sender, EventArgs e)
        {
            fr_pass.BorderColor = Color.White;
        }

        #endregion

        public async void ErrorAlert(string a)
        {
            await DisplayAlert("Error", a, "OK");
        }
    }
}