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
    public partial class EditPhonePage : ContentPage
    {
        #region Инициализация 
        private UserInfo user;
        private StackLayout layout;
        private MaskedEntry me_phone;
        private Frame fr_phone, fr_password;
        private Button bt_save;
        private ControlEntry en_password;
        private Label lb_main_fields, lb_phone, lb_phone_er, lb_password, lb_password_er;
        DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private Regex re_password = new Regex(RegexConstants.Password);
        #endregion
        public EditPhonePage()
        {
            Title = "Редактирование Телефона";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            user = DataControl.GetCurrentUserItem();


            #region Элементы страницы
            // Телефон
            lb_phone = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_phone);

            me_phone = new MaskedEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Mask = "+7 (XXX) XXX-XX-XX",
                Placeholder = "+7 (XXX) XXX-XX-XX",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                Background = null,
                BackgroundColor = Color.White,
                Keyboard = Keyboard.Telephone,
                VerticalTextAlignment = TextAlignment.Center
            };

            me_phone.TextChanged += OnPhoneTextChanged;
            me_phone.Unfocused += OnPhoneUnfocused;
            me_phone.Focused += OnPhoneFocusedChanged;

            fr_phone = new Frame
            {
                Content = me_phone,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };


            layout.Children.Add(fr_phone);

            // Неправильный формат телефона
            lb_phone_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат телефона",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_phone_er);

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

            // Пользователь с таким номером телефона уже зарегистрирован
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

            // Пользователь с таким номером телефона уже зарегистрирован
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

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (fr_phone.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }
        }

        public void OnPhoneTextChanged(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.White;
            lb_phone_er.IsVisible = false;
            fr_phone.BackgroundColor = Color.White;
            me_phone.BackgroundColor = Color.White;
            me_phone.TextColor = Color.Black;
        }

        public void OnPhoneFocusedChanged(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnPhoneUnfocused(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.White;
            if (me_phone.Text == "" || me_phone.Text == null) return;
            else
            {
                if (me_phone.Text.Length != 18)
                {
                    fr_phone.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_phone_er.IsVisible = true;
                    fr_phone.BackgroundColor = Color.FromRgb(255, 187, 187);
                    me_phone.BackgroundColor = Color.FromRgb(255, 187, 187);
                    me_phone.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_phone.BorderColor = Color.White;
                    lb_phone_er.IsVisible = false;
                    fr_phone.BackgroundColor = Color.White;
                    me_phone.BackgroundColor = Color.White;
                    me_phone.TextColor = Color.Black;
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
            if (en_password.Text == "" && me_phone.Text == "")
            {
                return;
            }
            else
            {
                if (fr_password.BorderColor == Color.FromRgb(194, 85, 85) || fr_phone.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    return;
                }
                else
                {
                    if (en_password.Text == "" || me_phone.Text == "")
                    {
                        lb_main_fields.IsVisible = true;
                    }
                    else
                    {
                        ChangePhoneRequest changePhone = new ChangePhoneRequest();
                        changePhone.NewPhoneNumber = me_phone.Text;
                        changePhone.Password = en_password.Text;

                        try
                        {
                            await dogsCompanionClient.ChangePhoneAsync(changePhone);
                            DataControl.SetUserInfoItem(await dogsCompanionClient.GetUserInfoAsync());
                            await Navigation.PopAsync();
                        }
                        catch (ApiException apiExc)
                        {
                            if (apiExc.StatusCode == StatusCodes.Status409Conflict)
                            {
                                await DisplayAlert("", "Номер телефона уже зарегистрирован", "OK");
                            }
                            else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                            {
                                await DisplayAlert("", "Неверный пароль", "OK");
                            }
                        }
                        catch (Exception exc)
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

                string phone = updateUser.PhoneNumber.ToString();
                if (phone.StartsWith("+7"))
                {
                    me_phone.Placeholder = phone;
                }
                else
                {
                    try
                    {
                        me_phone.Placeholder = "+7 (" + phone.Substring(1, 3) + ") " + phone.Substring(3, 3) + '-' + phone.Substring(7, 2) + '-' + phone.Substring(9, 2);
                    }
                    catch (Exception e)
                    {
                        me_phone.Placeholder = phone;
                    }
                }
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