using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmailPage : ContentPage
    {
        #region Инициализация 
        private StackLayout layout;
        private Label lb_email,lb_main_fields, lb_email_exists, lb_email_er;
        private ControlEntry en_email;
        private Frame fr_email;
        private Button bt_save;
        private Regex
            re_email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        #endregion
        public EditEmailPage()
        {
            Title = "Редактирование почты";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

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
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "ivanivanov@mail.ru",
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

            // Кнопка Сохранить изменения
            bt_save = new Button()
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

            // Пользователь с такой почтой уже зарегистрирован
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
            fr_email.BorderColor = Color.SpringGreen;
        }

        public void OnEmailUnfocused(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.White;
            if (en_email.Text == "" || en_email.Text == null) return;
            else
            {
                Match match = re_email.Match(en_email.Text);
                if (!match.Success)
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

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            //lb_email_exists и еще кучу всегоs
            await Navigation.PopAsync();
        }
        

        #endregion

    }
}