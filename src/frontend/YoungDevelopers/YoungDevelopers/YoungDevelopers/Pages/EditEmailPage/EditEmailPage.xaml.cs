using DogsCompanion.Api.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoungDevelopers.Utils;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmailPage : ContentPage
    {
        #region Инициализация 
        private UserInfo user;
        private StackLayout layout;
        private Label lb_email,lb_main_fields, lb_email_exists, lb_email_er, lb_password, lb_password_er;
        private ControlEntry en_email, en_password;
        private Frame fr_email, fr_password;
        private Button bt_save;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private Regex
            re_email = new Regex(RegexConstants.Email),
            re_password = new Regex(RegexConstants.Password);
        #endregion
        public EditEmailPage()
        {
            Title = "Редактирование почты";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            user = DataControl.GetCurrentUserItem();
            #region Элементы страницы

            // E-mail
            lb_email = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "E-Mail *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_email);

            en_email = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = user.Email,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                Keyboard = Keyboard.Email,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_email.TextChanged += OnEmailTextChanged;
            en_email.Unfocused += OnEmailUnfocused;
            en_email.Focused += OnEmailFocused;

            fr_email = new Frame
            {
                Content = en_email,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_email);

            // Ошибка при вводе почты
            lb_email_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат почты",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_email_er);

            // Пароль
            lb_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Пароль *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_password);

            en_password = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "**********",
                IsPassword = true,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_password.TextChanged += OnPasswordTextChanged;
            en_password.Unfocused += OnPasswordUnfocused;
            en_password.Focused += OnPasswordFocused;

            fr_password = new Frame
            {
                Content = en_password,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_password);

            // Ошибка при вводе пароля
            lb_password_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = ErrorConstants.PasswordRequirements,
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_password_er);


            // Кнопка Сохранить изменения
            bt_save = new Button()
            {
                Text = "Сохранить изменения",
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

            layout.Children.Add(bt_save);
            bt_save.Clicked += OnSaveClicked;

            // Не заполнены обязательные поля
            lb_main_fields = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Не заполнены обязательные поля",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_main_fields);

            // Пользователь с такой почтой уже зарегистрирован / пароль неправильный
            lb_email_exists = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Пользователь с такой почтой уже зарегистрирован",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_email_exists);

            #endregion

            this.Content = layout;
            InitializeComponent();
            UpdateFieldsFromServer();
        }

        #region Обработка событий

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (fr_email.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }

        }

        public void OnEmailTextChanged(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.White;
            lb_email_er.IsVisible = false;
            fr_email.BackgroundColor = Color.White;
            en_email.BackgroundColor = Color.White;
            en_email.TextColor = Color.Black;
        }

        public void OnEmailFocused(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnEmailUnfocused(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.White;
            if (en_email.Text == "" || en_email.Text == null) return;
            else
            {
                Match match = re_email.Match(en_email.Text);
                if (!match.Success || en_email.Text.Length > 320)
                {
                    lb_email_er.IsVisible = true;
                    fr_email.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_email.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_email.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_email.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    lb_email_er.IsVisible = false;
                    fr_email.BorderColor = Color.White;
                    fr_email.BackgroundColor = Color.White;
                    en_email.BackgroundColor = Color.White;
                    en_email.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        public void OnPasswordTextChanged(object sender, EventArgs e)
        {
            lb_password_er.IsVisible = false;
            fr_password.BorderColor = Color.White;
            fr_password.BackgroundColor = Color.White;
            en_password.BackgroundColor = Color.White;
            en_password.TextColor = Color.Black;
        }

        public void OnPasswordFocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnPasswordUnfocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.White;
            if (en_password.Text == "" || en_password.Text == null) return;
            else
            {
                Match match = re_password.Match(en_password.Text);
                if (!match.Success)
                {
                    lb_password_er.IsVisible = true;
                    fr_password.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_password.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    lb_password_er.IsVisible = false;
                    fr_password.BorderColor = Color.White;
                    fr_password.BackgroundColor = Color.White;
                    en_password.BackgroundColor = Color.White;
                    en_password.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            lb_main_fields.IsVisible = false;
            if (en_email.Text == "" && en_password.Text == "")
            {
                return;
            }
            else
            {
                if (fr_email.BorderColor == Color.FromRgb(194, 85, 85) || fr_password.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    return;
                }
                else
                {
                    if (en_email.Text == "" || en_password.Text == "")
                    {
                        lb_main_fields.IsVisible = true;
                    }
                    else
                    {
                        ChangeEmailRequest changeEmail = new ChangeEmailRequest();
                        changeEmail.Email = en_email.Text;
                        changeEmail.Password = en_password.Text;

                        try
                        {
                            await dogsCompanionClient.ChangeEmailAsync(changeEmail);
                            DataControl.SetUserInfoItem(await dogsCompanionClient.GetUserInfoAsync());
                            await Navigation.PopAsync();
                        }
                        catch (ApiException apiExc)
                        {
                            if (apiExc.StatusCode == StatusCodes.Status409Conflict)
                            {
                                await DisplayAlert("", "Почта уже используется", "OK");
                            }
                            else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                            {
                                await DisplayAlert("", "Неверный пароль", "OK");
                            }
                            else if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                            {
                                await DisplayAlert("", "Сервис недоступен", "OK");
                            }
                        }
                        catch
                        {
                            await DisplayAlert("", "Непредвиденная ошибка", "OK");
                        }
                    }
                }
            }
        }


        #endregion

        public async void UpdateFieldsFromServer()
        {
            try
            {
                UserInfo updateUser = (UserInfo)await dogsCompanionClient.GetUserInfoAsync();

                en_email.Placeholder = updateUser.Email.ToString();
            }
            catch (ApiException apiExc)
            {
                await DisplayAlert("", "Сервис недоступен", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("", "Непредвиденная ошибка", "OK");
            }
        }
    }
}
