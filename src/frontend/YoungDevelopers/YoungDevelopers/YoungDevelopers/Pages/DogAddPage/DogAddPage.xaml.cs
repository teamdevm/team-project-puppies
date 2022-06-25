using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using DogsCompanion.Api.Client;
using System.Net.Http;
using YoungDevelopers.Client;
using Microsoft.AspNetCore.Http;

namespace YoungDevelopers
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DogAddPage : ContentPage
    {
        #region Инициализация 
        private ActivityIndicator activityIndicator;
        private StackLayout main, layout;
        private ScrollView scrollview;
        private Frame fr_nickname, fr_breed, fr_weight, fr_birthdate;
        private ControlEntry en_nickname, en_breed, en_weight;
        private DatePickerControl dp_birthdate;
        private Label lb_musthave, lb_nickname, lb_breed, lb_weight, lb_birthdate, lb_nickname_er, lb_breed_er, lb_weight_er,
            lb_main_fields, lb_reg_er;
        private Button bt_registrate;
        private SendRegistraion userInfo;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private HttpClient httpClient = DataControl.httpClient;
        private TokenController tokenController = DataControl.tokenController;
        private Regex
            re_nickname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$"),
            re_weight = new Regex("[+-]?([0-9]*[.])?[0-9]+");
        #endregion

        public DogAddPage(SendRegistraion userInfo_input)
        {
            this.Title = "Регистрация собаки";
            layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };
            scrollview = new ScrollView();
            main = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };
            userInfo = userInfo_input;
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            // Label обязательные поля
            lb_musthave = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                Text = "* Обязательное поле",
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(15, 5, 0, 15),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_musthave);

            // Кличка собаки
            lb_nickname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Кличка собаки *",
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
                Placeholder = "Стасян",
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
                Text = "Порода собаки *",
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
                Placeholder = "Мопсик",
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
                Text = "Вес собаки в кг. *",
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
                Placeholder = "15",
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
                Text = "Вес собаки должен быть от 1 до 200 кг",
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
                Date = DateTime.Now,
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-5, -15, 0, -12),
                TextColor = Color.Gray,
                Format = "dd.MM.yyyy",
            };

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

            activityIndicator = new ActivityIndicator
            {
                IsVisible = false,
                IsRunning = false,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Color = Color.FromRgb(105, 233, 165)
            };

            layout.Children.Add(activityIndicator);

            // Кнопка регистрация
            bt_registrate = new Button()
            {
                Text = "Создать аккаунт",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(105,233,165),
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 225, 0, 10),
            };

            layout.Children.Add(bt_registrate);
            bt_registrate.Clicked += OnRegistrateClicked;

            // Не заполнены обязательные поля 
            lb_main_fields = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Не заполнены обязательные поля",
                Margin = new Thickness(15, -10, 0, 5),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_main_fields);

            // Ошибка регистрации
            lb_reg_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_reg_er);

            #endregion

            // Контент страницы
            scrollview.Content = layout;
            scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
            main.Children.Add(scrollview);
            this.Content = main;
            InitializeComponent();
        }

        #region Обработка событий

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (en_nickname.Text != "" && en_breed.Text != "" && en_weight.Text != "" && fr_nickname.BorderColor == Color.White && fr_breed.BorderColor == Color.White && fr_weight.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }

        }

        private void OnUploadClicked(object sender, EventArgs e)
        {

        }

        private async void OnRegistrateClicked(object sender, EventArgs e)
        {
            lb_main_fields.IsVisible = false;
            if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy") && en_nickname.Text == "" && en_breed.Text == "" && en_weight.Text == "")
            {
                
                return;
            }
            else
            {
                if (fr_nickname.BorderColor == Color.FromRgb(194, 85, 85) || fr_breed.BorderColor == Color.FromRgb(194, 85, 85) || fr_weight.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    return;
                }
                else
                {
                    if (en_nickname.Text == "" || en_breed.Text == "" || en_weight.Text == "")
                    {
                        lb_main_fields.IsVisible = true;
                        return;
                    }
                    bt_registrate.IsVisible = false;
                    activityIndicator.IsVisible = true;
                    activityIndicator.IsRunning = true;
                    
                    UpdateDog createDog = new UpdateDog();
                    createDog.Name = en_nickname.Text;
                    createDog.Breed = en_breed.Text;
                    createDog.Weight = Int32.Parse(en_weight.Text);
                    if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy"))
                    {
                        createDog.BirthDate = null;
                    }
                    else
                    {
                        createDog.BirthDate = dp_birthdate.Date;
                    }

                    RegisterInfo registerInfo = new RegisterInfo();
                    registerInfo.FirstName = userInfo.firstname;
                    registerInfo.LastName = userInfo.lastname;
                    registerInfo.MiddleName = userInfo.patronymic;
                    registerInfo.Email = userInfo.email;
                    registerInfo.BirthDate = userInfo.birthdate;
                    registerInfo.PhoneNumber = userInfo.phone;
                    registerInfo.Password = userInfo.password;
                    registerInfo.Dog = createDog;

                    try
                    {
                        RegisterResponse response = await dogsCompanionClient.RegisterUserAsync(registerInfo);


                        // Установление ключа в httpclient
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.AccessToken);

                        // Сохранение токенов в хранилище
                        await tokenController.SetRefreshTokenAsync(response.RefreshToken);
                        await tokenController.SetAccessTokenAsync(response.AccessToken);
                                             
                        // Установить текущего пользователя АВАВАВА ОСУЖДАЮ
                        DataControl.SetNewCurrentUser(response);

                        bt_registrate.IsVisible = true;
                        activityIndicator.IsVisible = false;
                        activityIndicator.IsRunning = false;


                        App.Current.MainPage = new MainPage();
                    }
                    catch (ApiException apiExc)
                    {

                        bt_registrate.IsVisible = true;
                        activityIndicator.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        if (apiExc.StatusCode == StatusCodes.Status409Conflict)
                        {
                            await DisplayAlert("Ошибка", "Почта или номер телефона уже используется", "OK");
                        }
                        else if (apiExc.StatusCode == StatusCodes.Status401Unauthorized)
                        {
                            await DisplayAlert("Ошибка", "Номер телефона уже используется", "OK");
                        }
                        else if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)

                        {
                            lb_reg_er.Text = "Сервис недоступен";
                            lb_reg_er.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Ошибка", apiExc.Response, "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        bt_registrate.IsVisible = true;
                        activityIndicator.IsVisible = false;
                        activityIndicator.IsRunning = false;
                        await DisplayAlert("Ошибка", ex.Message, "OK");

                    }
                }             
            }
        }

        private void OnNicknameFocused(object sender, EventArgs e)
        {
            fr_nickname.BorderColor = Color.FromRgb(105,233,165);
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
                    CheckRecField();
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
            fr_breed.BorderColor = Color.FromRgb(105,233,165);
        }

        private void OnBreedUnfocused(object sender, EventArgs e)
        {
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
                    CheckRecField();
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
            fr_weight.BorderColor = Color.FromRgb(105,233,165);
        }

        private void OnWeightUnfocused(object sender, EventArgs e)
        {
            if (en_weight.Text == "" || en_weight.Text == null) return;
            else
            {
                Match match = re_weight.Match(en_weight.Text);
                if (!match.Success || int.Parse(en_weight.Text) < 1 || int.Parse(en_weight.Text) > 200)
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
                    CheckRecField();
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

        #endregion
    }
}