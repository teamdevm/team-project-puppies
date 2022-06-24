using DogsCompanion.Api.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDogeProfilePage : ContentPage
    {
        #region Инициализация 
        private int userID;
        private ReadDog UserDog;
        private bool hasDate = false;
        private StackLayout layout;
        private ScrollView scrollview;
        private Frame fr_nickname, fr_breed, fr_weight, fr_birthdate;
        private ControlEntry en_nickname, en_breed, en_weight;
        private DatePickerControl dp_birthdate;
        private Label lb_musthave, lb_nickname, lb_breed, lb_weight, lb_birthdate, lb_nickname_er, lb_breed_er, lb_weight_er,
            lb_update_er;
        private Button bt_registrate;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private Regex
            re_nickname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$"),
            re_weight = new Regex("[+-]?([0-9]*[.])?[0-9]+");

        #endregion

        public EditDogeProfilePage()
        {
            this.Title = "Обновление данных собаки";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            App.Current.Properties["currentuserid"] = 1;
            userID = (int)App.Current.Properties["currentuserid"];
            UserDog = DataControl.GetUserDogItem(userID);


            #region Элементы страницы

            // Кличка собаки
            lb_nickname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Кличка собаки",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_nickname);

            en_nickname = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = UserDog.Name,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_nickname.TextChanged += OnNicknameTextChanged;
            en_nickname.Focused += OnNicknameFocused;
            en_nickname.Unfocused += OnNicknameUnfocused;

            fr_nickname = new Frame()
            {
                Content = en_nickname,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_nickname);

            // Ошибка при вводе клички
            lb_nickname_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат клички",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_nickname_er);

            // Порода собаки
            lb_breed = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Порода собаки",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_breed);

            en_breed = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = UserDog.Breed,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_breed.TextChanged += OnBreedTextChanged;
            en_breed.Focused += OnBreedFocused;
            en_breed.Unfocused += OnBreedUnfocused;

            fr_breed = new Frame()
            {
                Content = en_breed,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_breed);

            // Ошибка при вводе породы
            lb_breed_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат породы",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_breed_er);

            // Вес собаки
            lb_weight = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Вес собаки в кг.",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_weight);

            en_weight = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = UserDog.Weight.ToString(),
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                Keyboard = Keyboard.Numeric
            };

            en_weight.TextChanged += OnWeightTextChanged;
            en_weight.Focused += OnWeightFocused;
            en_weight.Unfocused += OnWeightUnfocused;

            fr_weight = new Frame()
            {
                Content = en_weight,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_weight);

            // Ошибка при вводе породы
            lb_weight_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат веса собаки",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_weight_er);

            // Дата рождения собаки
            lb_birthdate = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Дата рождения собаки",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_birthdate);

            dp_birthdate = new DatePickerControl()
            {
                Date = UserDog.BirthDate == null ? DateTime.UtcNow : DateTime.Parse(UserDog.BirthDate.ToString()),
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-5, -15, 0, -12),
                TextColor = Color.Gray,
                Format = "dd.MM.yyyy",
            };

            dp_birthdate.DateSelected += OnDateSelected;

            fr_birthdate = new Frame
            {
                Content = dp_birthdate,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_birthdate);

            // Кнопка регистрация

            bt_registrate = new Button()
            {
                Text = "Сохранить изменения",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_registrate);
            bt_registrate.Clicked += OnSaveClicked;
                       
            lb_update_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Ошибка при обновлении информации",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_update_er);

            #endregion

            // Контент страницы
            scrollview = new ScrollView();
            scrollview.Content = layout;
            scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
            this.Content = scrollview;

            InitializeComponent();
            UpdateFieldsFromServer();
        }

        #region Обработка событий

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            lb_update_er.IsVisible = false;
            if ((dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.UtcNow.Date.ToString("dd.MM.yyyy") || dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Parse(UserDog.BirthDate.ToString()).ToString("dd.MM.yyyy")) && en_breed.Text == "" && en_nickname.Text == "" && en_weight.Text == "")
            {
                
                return;
            }
            else 
            {
                if (fr_nickname.BorderColor == Color.FromRgb(194, 85, 85) || fr_breed.BorderColor == Color.FromRgb(194, 85, 85) || fr_weight.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    await DisplayAlert("Error", "Все косяк", "OK");
                    return;
                }
                else
                {
                    UpdateDog updateDog = new UpdateDog();

                    if (en_nickname.Text == "")
                    {
                        updateDog.Name = UserDog.Name;
                    }
                    else
                    {
                        updateDog.Name = en_nickname.Text;
                    }

                    if (en_breed.Text == "")
                    {
                        updateDog.Breed = UserDog.Breed;
                    }
                    else
                    {
                        updateDog.Breed = en_breed.Text;
                    }

                    if (en_weight.Text == "")
                    {
                        updateDog.Weight = UserDog.Weight;
                    }
                    else
                    {
                        updateDog.Weight = Int32.Parse(en_weight.Text);
                    }

                    if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.UtcNow.Date.ToString("dd.MM.yyyy"))
                    {
                        updateDog.BirthDate = UserDog.BirthDate;
                    }
                    else
                    {
                        //updateDog.BirthDate = new DateTimeOffset(dp_birthdate.Date);
                        updateDog.BirthDate = UserDog.BirthDate;
                    }

                    try
                    {
                        await dogsCompanionClient.PutDogAsync(updateDog);
                        await Navigation.PopAsync();
                    }
                    catch (ApiException apiExc)
                    {
                        if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                        {
                            lb_update_er.Text = "Сервис недоступен";
                            lb_update_er.IsVisible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                }
            }
            //
            //await Navigation.PopAsync();
        }

        private void OnNicknameFocused(object sender, EventArgs e)
        {
            fr_nickname.BorderColor = Color.SpringGreen;
        }

        private void OnNicknameUnfocused(object sender, EventArgs e)
        {
            fr_nickname.BorderColor = Color.White;
            if (en_nickname.Text == "" || en_nickname.Text == null) return;
            else
            {
                Match match = re_nickname.Match(en_nickname.Text);
                if (!match.Success)
                {
                    fr_nickname.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_nickname_er.IsVisible = true;
                    fr_nickname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_nickname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_nickname.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_nickname.BorderColor = Color.White;
                    lb_nickname_er.IsVisible = false;
                    fr_nickname.BackgroundColor = Color.White;
                    en_nickname.BackgroundColor = Color.White;
                    en_nickname.TextColor = Color.Black;
                }
            }
        }

        private void OnNicknameTextChanged(object sender, EventArgs e)
        {
            lb_nickname_er.IsVisible = false;
            fr_nickname.BorderColor = Color.White;
            fr_nickname.BackgroundColor = Color.White;
            en_nickname.BackgroundColor = Color.White;
            en_nickname.TextColor = Color.Black;
        }

        private void OnBreedFocused(object sender, EventArgs e)
        {
            fr_breed.BorderColor = Color.SpringGreen;
        }

        private void OnBreedUnfocused(object sender, EventArgs e)
        {
            fr_breed.BorderColor = Color.White;
            if (en_breed.Text == "" || en_breed.Text == null) return;
            else
            {
                Match match = re_nickname.Match(en_breed.Text);
                if (!match.Success)
                {
                    fr_breed.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_breed_er.IsVisible = true;
                    fr_breed.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_breed.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_breed.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_breed.BorderColor = Color.White;
                    lb_breed_er.IsVisible = false;
                    fr_breed.BackgroundColor = Color.White;
                    en_breed.BackgroundColor = Color.White;
                    en_breed.TextColor = Color.Black;
                }
            }
        }

        private void OnBreedTextChanged(object sender, EventArgs e)
        {
            lb_breed_er.IsVisible = false;
            fr_breed.BorderColor = Color.White;
            fr_breed.BackgroundColor = Color.White;
            en_breed.BackgroundColor = Color.White;
            en_breed.TextColor = Color.Black;
        }

        private void OnWeightFocused(object sender, EventArgs e)
        {
            fr_weight.BorderColor = Color.SpringGreen;
        }

        private void OnWeightUnfocused(object sender, EventArgs e)
        {
            fr_weight.BorderColor = Color.White;
            if (en_weight.Text == "" || en_weight.Text == null) return;
            else
            {
                Match match = re_weight.Match(en_weight.Text);
                if (!match.Success)
                {
                    fr_weight.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_weight_er.IsVisible = true;
                    fr_weight.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_weight.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_weight.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_weight.BorderColor = Color.White;
                    lb_weight_er.IsVisible = false;
                    fr_weight.BackgroundColor = Color.White;
                    en_weight.BackgroundColor = Color.White;
                    en_weight.TextColor = Color.Black;
                }
            }
        }

        private void OnWeightTextChanged(object sender, EventArgs e)
        {
            lb_weight_er.IsVisible = false;
            fr_weight.BorderColor = Color.White;
            fr_weight.BackgroundColor = Color.White;
            en_weight.BackgroundColor = Color.White;
            en_weight.TextColor = Color.Black;
        }

        public void OnDateSelected(object sender, EventArgs e)
        {
            hasDate = true;
        }

        #endregion

        public async void UpdateFieldsFromServer()
        {
            #region подгрузка
            string email = "spistsov@gmail.com";
            string password = "Billy1!";
            SendAuth sendauth = new SendAuth();
            sendauth.email = email;
            sendauth.password = password;

            var httpClient = DataControl.httpClient;
            var tokenController = DataControl.tokenController;


            try
            {
                var authInfo = new AuthInfo()
                {
                    Email = email,
                    Password = password,
                };

                var authResponse = await dogsCompanionClient.AuthenticateAsync(authInfo);

                // Установление ключа в httpclient
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponse.AccessToken);

                // Сохранение токенов в хранилище
                await tokenController.SetRefreshTokenAsync(authResponse.RefreshToken);
                await tokenController.SetAccessTokenAsync(authResponse.AccessToken);

                // Установить текущего пользователя
                DataControl.SetCurrentUser(authResponse);

                // Переход на главную страницу
                // TODO сохранить полученные данные из authInfo
                // TODO поменять Main страницу
            }
            catch (ApiException apiExc)
            {
                if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                {
                }
                else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                {
                }
            }
            catch (Exception)
            {
            }
            #endregion
            UserDog = await dogsCompanionClient.GetDogsAsync();

            en_nickname.Placeholder = UserDog.Name;
            en_breed.Placeholder = UserDog.Breed;
            en_weight.Placeholder = UserDog.Weight.ToString();
            dp_birthdate.Date = UserDog.BirthDate == null ? DateTime.UtcNow : DateTime.Parse(UserDog.BirthDate.ToString()); 

            // Заполнить поля

            // УЧЕСТЬ ПОЛЯ ПОЛЬЗОВАТЕЛЯ - СОБАКА
            // А ЕЩЕ ЕСТЬ СПИСОК СОБАК
        }
    }
}