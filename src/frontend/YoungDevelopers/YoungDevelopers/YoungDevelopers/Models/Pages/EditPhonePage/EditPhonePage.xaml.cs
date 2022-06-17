using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPhonePage : ContentPage
    {
        #region Инициализация 
        private StackLayout layout;
        private MaskedEntry me_phone;
        private Frame fr_phone;
        private Button bt_save;
        private Label lb_main_fields, lb_phone, lb_phone_er, lb_phone_exists;
        #endregion
        public EditPhonePage()
        {
            Title = "Редактирование Телефона";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

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


            // Пользователь с таким номером телефона уже зарегистрирован
            lb_phone_exists = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Пользователь с такой почтой уже зарегистрирован",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_phone_exists);
            #endregion

            this.Content = layout;
            InitializeComponent();
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
            fr_phone.BorderColor = Color.SpringGreen;
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

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            //lb_phone_exists и еще кучу всего

            // ВЫЛЕТАЕТ ПАКОСТЬ
            await Navigation.PopAsync();
        }

        #endregion
    }
}