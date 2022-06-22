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
        private bool hasDate = false;
        private StackLayout layout;
        private ScrollView scrollview;
        private Frame fr_nickname, fr_breed, fr_weight, fr_birthdate;
        private ControlEntry en_nickname, en_breed, en_weight;
        private DatePickerControl dp_birthdate;
        private Label lb_musthave, lb_nickname, lb_breed, lb_weight, lb_birthdate, lb_nickname_er, lb_breed_er, lb_weight_er,
            lb_main_fields;
        private Button bt_registrate;
        private SendDogRegistration senddogreg;
        private Regex
            re_nickname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$"),
            re_weight = new Regex("[+-]?([0-9]*[.])?[0-9]+");

        #endregion

        public EditDogeProfilePage()
        {
            this.Title = "Регистрация собаки";
            layout = new StackLayout();
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
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "15.0",
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
            bt_registrate.Clicked += OnRegistrateClicked;

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

            #endregion

            // Контент страницы
            scrollview = new ScrollView();
            scrollview.Content = layout;
            scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
            this.Content = scrollview;

            InitializeComponent();
        }

        #region Обработка событий

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (fr_nickname.BorderColor == Color.White && fr_breed.BorderColor == Color.White && fr_birthdate.BorderColor == Color.White && fr_weight.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }

        }

        private async void OnRegistrateClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            if (fr_nickname.BackgroundColor == Color.FromRgb(194, 85, 85) || fr_breed.BackgroundColor == Color.FromRgb(194, 85, 85) || fr_weight.BackgroundColor == Color.FromRgb(194, 85, 85) ||
                lb_main_fields.IsVisible == true)
            {
                lb_main_fields.IsVisible = true;
            }
            else if (en_nickname.Text == "" || en_nickname.Text == null || en_breed.Text == "" || en_breed.Text == null || en_weight.Text == "" || en_weight.Text == null)
            {
                lb_main_fields.IsVisible = true;
            }
            else
            {
                lb_main_fields.IsVisible = false;

                senddogreg = new SendDogRegistration();

                senddogreg.nickname = en_nickname.Text;
                senddogreg.breed = en_breed.Text;
                if (hasDate)
                {
                    senddogreg.birthdate = dp_birthdate.Date.ToString("dd.MM.yyyy");
                }
                senddogreg.weight = en_weight.Text;

                //Вася ты где блин ну

                await Navigation.PushAsync(new MainPage());
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