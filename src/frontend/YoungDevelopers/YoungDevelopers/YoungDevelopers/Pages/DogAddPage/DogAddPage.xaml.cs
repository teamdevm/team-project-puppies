using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.Linq;
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
        private bool hasDate = false;
        private StackLayout main, layout;
        private ScrollView scrollview;
        private Image im_doge;
        private Frame fr_nickname, fr_breed, fr_weight, fr_birthdate;
        private ControlEntry en_nickname, en_breed, en_weight;
        private DatePickerControl dp_birthdate;
        private Label lb_musthave, lb_nickname, lb_breed, lb_weight, lb_birthdate, lb_nickname_er, lb_breed_er, lb_weight_er,
            lb_main_fields, lb_reg_er;
        private Button bt_registrate, bt_upload_doge;
        private SendRegistraion userInfo;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private HttpClient httpClient = DataControl.httpClient;
        private TokenController tokenController = DataControl.tokenController;
        private Regex
            re_nickname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$"),
            re_weight = new Regex("[+-]?([0-9]*[.])?[0-9]+");

        #endregion

        //SendRegistraion RegFields
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

            // Шаблон для добавления изображения собаки
            im_doge = new Image
            {
                Source = "ben" +
                ".jpg",
                Margin = new Thickness(50, 20, 50, 0),
                BackgroundColor = Color.FromRgb(242, 242, 242),
            };

            layout.Children.Add(im_doge);

            // Кнопка добавить изображения
            bt_upload_doge = new Button
            {
                Text = "Загрузить",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 150
            };
            bt_upload_doge.Clicked += OnUploadClicked;

            layout.Children.Add(bt_upload_doge);

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
                Date = DateTime.Now,
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
                Text = "Создать аккаунт",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 15, 0, 10),
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
            //await Navigation.PushAsync(new MainPageDoge());
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
                    //RegisterInfo
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

                        await DisplayAlert("Error", response.IsEmailInUse.ToString(), "OK");
                        if (response.IsEmailInUse)
                        {
                            lb_reg_er.Text = "Такой E-mail уже зарегистрирован";
                            await DisplayAlert("Error", "email is use", "OK");
                            lb_reg_er.IsVisible = true;
                            return;
                        }

                        if (response.IsPhoneInUse)
                        {
                            lb_reg_er.Text = "Пользователь с таким номером телефона уже зарегистрирован";
                            lb_reg_er.IsVisible = true;
                            return;
                        }

                        // Установление ключа в httpclient
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.AccessToken);

                        // Сохранение токенов в хранилище
                        await tokenController.SetRefreshTokenAsync(response.RefreshToken);
                        await tokenController.SetAccessTokenAsync(response.AccessToken);
                                             

                        // Установить текущего пользователя
                        DataControl.SetNewCurrentUser(response);
                        App.Current.MainPage = new MainPage();
                    }
                    catch (ApiException apiExc)
                    {
                        if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                        {
                            lb_reg_er.Text = "Сервис недоступен";
                            lb_reg_er.IsVisible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                }             
            }
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
            fr_breed.BorderColor = Color.SpringGreen;
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
            fr_weight.BorderColor = Color.SpringGreen;
        }

        private void OnWeightUnfocused(object sender, EventArgs e)
        {
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

        public void OnDateSelected(object sender, EventArgs e)
        {
            hasDate = true;
        }

        #endregion
    }
}