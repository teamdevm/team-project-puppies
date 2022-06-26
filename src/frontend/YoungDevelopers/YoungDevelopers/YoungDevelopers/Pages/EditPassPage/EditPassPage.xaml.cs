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
    public partial class EditPassPage : ContentPage
    {
        #region Инициализация 
        private UserInfo user;
        private StackLayout layout;
        private Button bt_save;
        private Frame fr_password, fr_rep_password, fr_old_password;
        private Label lb_old_password, lb_old_password_er, lb_password, lb_rep_password, lb_password_er, lb_rep_password_er, lb_conc_pass_er, lb_main_fields;
        private ControlEntry en_password, en_rep_password, en_old_password;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private Regex
            re_password = new Regex(RegexConstants.Password);
        #endregion
        public EditPassPage()
        {
            Title = "Редактирование пароля";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            user = DataControl.GetCurrentUserItem();

            #region Элементы страницы

            // Старый пароль

            lb_old_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Старый пароль *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 10, 0, 5),
            };
            layout.Children.Add(lb_old_password);

            en_old_password = new ControlEntry()
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

            en_old_password.TextChanged += OnOldPasswordTextChanged;
            en_old_password.Unfocused += OnOldPasswordUnfocused;
            en_old_password.Focused += OnOldPasswordFocused;

            fr_old_password = new Frame
            {
                Content = en_old_password,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_old_password);

            // Ошибка при вводе пароля
            lb_old_password_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = ErrorConstants.PasswordRequirements,
                Margin = new Thickness(15, -5, 10, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_old_password_er);

            // Пароль
            lb_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Новый Пароль *",
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
                Margin = new Thickness(15, -5, 10, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_password_er);

            // Повторить пароль
            lb_rep_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Повторите пароль *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_rep_password);

            en_rep_password = new ControlEntry()
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

            en_rep_password.TextChanged += OnRepPasswordTextChanged;
            en_rep_password.Unfocused += OnRepPasswordUnfocused;
            en_rep_password.Focused += OnRepPasswordFocused;

            fr_rep_password = new Frame
            {
                Content = en_rep_password,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_rep_password);

            // Ошибка при вводе пароля
            lb_rep_password_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат пароля",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_rep_password_er);

            // Ошибка несовпадения
            lb_conc_pass_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Пароли не совпадают",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_conc_pass_er);

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

            #endregion

            this.Content = layout;
            InitializeComponent();
            UpdateFieldsFromServer();
        }

        #region Обработка событий

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            lb_main_fields.IsVisible = false;
            if ((en_password.Text == "" && en_rep_password.Text == "" && en_old_password.Text == "") || lb_conc_pass_er.IsVisible == true)
            {
                return;
            }
            else
            {
                if (fr_password.BorderColor == Color.FromRgb(194, 85, 85) || fr_rep_password.BorderColor == Color.FromRgb(194, 85, 85) || fr_old_password.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    return;
                }
                else
                {
                    if (en_password.Text == "" || en_rep_password.Text == "" || en_old_password.Text == "")
                    {
                        lb_main_fields.IsVisible = true;
                    }
                    else
                    {
                        ChangePasswordRequest changePass = new ChangePasswordRequest();
                        changePass.NewPassword = en_password.Text;
                        changePass.CurrentPassword = en_old_password.Text;

                        try
                        {
                            await dogsCompanionClient.ChangePasswordAsync(changePass);
                            await Navigation.PopAsync();
                        }
                        catch (ApiException apiExc)
                        {
                            if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                            {
                                await DisplayAlert("", "Неверный текущий пароль", "OK");
                            }
                            else if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                            {
                                await DisplayAlert("", "Сервис недоступен", "OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("", "Непредвиденная ошибка", "OK");
                        }
                    }
                }
            }
        }

        public void OnOldPasswordTextChanged(object sender, EventArgs e)
        {
            lb_old_password_er.IsVisible = false;
            fr_old_password.BorderColor = Color.White;
            fr_old_password.BackgroundColor = Color.White;
            en_old_password.BackgroundColor = Color.White;
            en_old_password.TextColor = Color.Black;
        }

        public void OnOldPasswordFocused(object sender, EventArgs e)
        {
            fr_old_password.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnOldPasswordUnfocused(object sender, EventArgs e)
        {
            fr_old_password.BorderColor = Color.White;
            if (en_old_password.Text == "") return;
            else
            {
                Match match = re_password.Match(en_old_password.Text);
                if (!match.Success)
                {
                    lb_old_password_er.IsVisible = true;
                    fr_old_password.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_old_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_old_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_old_password.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    lb_old_password_er.IsVisible = false;
                    fr_old_password.BorderColor = Color.White;
                    fr_old_password.BackgroundColor = Color.White;
                    en_old_password.BackgroundColor = Color.White;
                    en_old_password.TextColor = Color.Black;
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

            if (lb_conc_pass_er.IsVisible == true && fr_rep_password.BorderColor == Color.White && en_rep_password.Text != "" && en_rep_password.Text != null)
            {
                if (en_password.Text == en_rep_password.Text)
                {
                    lb_conc_pass_er.IsVisible = false;
                }
            }
        }

        public void OnPasswordFocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnPasswordUnfocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.White;
            if (en_password.Text == "" || en_rep_password.Text == null) return;
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
                else if (en_rep_password.Text != en_password.Text && en_rep_password.Text != null && en_rep_password.Text != "")
                {
                    lb_conc_pass_er.IsVisible = true;
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

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (lb_conc_pass_er.IsVisible == false && fr_password.BorderColor == Color.White && fr_rep_password.BorderColor == Color.White && fr_old_password.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }
        }

        public void OnRepPasswordTextChanged(object sender, EventArgs e)
        {
            lb_rep_password_er.IsVisible = false;
            fr_rep_password.BorderColor = Color.White;
            fr_rep_password.BackgroundColor = Color.White;
            en_rep_password.BackgroundColor = Color.White;
            en_rep_password.TextColor = Color.Black;

            if (lb_conc_pass_er.IsVisible == true && fr_password.BorderColor == Color.White && en_password.Text != "" && en_password.Text != null)
            {
                if (en_password.Text == en_rep_password.Text)
                {
                    lb_conc_pass_er.IsVisible = false;
                }
            }
        }

        public void OnRepPasswordFocused(object sender, EventArgs e)
        {
            fr_rep_password.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnRepPasswordUnfocused(object sender, EventArgs e)
        {
            fr_rep_password.BorderColor = Color.White;
            if (en_password.Text == "" || en_rep_password.Text == null) return;
            else
            {
                Match match = re_password.Match(en_rep_password.Text);
                if (!match.Success)
                {
                    lb_rep_password_er.IsVisible = true;
                    fr_rep_password.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_rep_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_rep_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_rep_password.TextColor = Color.FromRgb(194, 85, 85);
                }
                else if (en_rep_password.Text != en_password.Text && en_password.Text != null && en_password.Text != "")
                {
                    lb_conc_pass_er.IsVisible = true;
                }
                else
                {
                    lb_rep_password_er.IsVisible = false;
                    fr_rep_password.BorderColor = Color.White;
                    fr_rep_password.BackgroundColor = Color.White;
                    en_rep_password.BackgroundColor = Color.White;
                    en_rep_password.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        #endregion

        public async void UpdateFieldsFromServer()
        {
            try
            {
                UserInfo updateUser = (UserInfo)await dogsCompanionClient.GetUserInfoAsync();
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