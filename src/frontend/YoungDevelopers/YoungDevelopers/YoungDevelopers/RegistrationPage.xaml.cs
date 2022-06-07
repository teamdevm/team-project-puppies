using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private ScrollView scrollview;
        private StackLayout layout, sl_approve;
        private Label lb_musthave, lb_userreg, lb_lastname, lb_firstname, lb_patronymic, lb_birthdate, lb_phone, lb_email, lb_password, lb_rep_password, lb_approve;
        private ControlEntry en_lastname, en_firstname, en_patronymic, en_email, en_password, en_rep_password;
        private Frame fr_lastname, fr_firstname, fr_patronymic, fr_birthdate, fr_phone, fr_email, fr_password, fr_rep_password;
        private Button bt_next;
        private DatePickerControl dp_birthdate;
        private MaskedEntry me_phone;
        private CheckBox cb_approve;
        // CheckBox Согласие
        public RegistrationPage()
        {
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.LightGray;

            // Label Регистрация
            lb_userreg = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Регистрация пользователя",
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                Margin = new Thickness(0, 5, 0, 15),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_userreg);

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

            // Фамилия
            lb_lastname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Фамилия",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_lastname);

            en_lastname = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder  = "Иванов",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            fr_lastname = new Frame
            {
                Content = en_lastname,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_lastname);

            // Имя
            lb_firstname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Имя",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_firstname);

            en_firstname = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "Иван",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            fr_firstname = new Frame
            {
                Content = en_firstname,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_firstname);

            // Отчество
            lb_patronymic = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Отчество",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_patronymic);

            en_patronymic = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "Иванович",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            fr_patronymic = new Frame
            {
                Content = en_patronymic,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_patronymic);

            // Дата рождения
            lb_birthdate = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Дата рождения",
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

            fr_birthdate = new Frame
            {
                Content = dp_birthdate,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_birthdate);

            // Телефон
            lb_phone = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон",
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

            fr_phone = new Frame
            {
                Content = me_phone,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_phone);

            // E-mail
            lb_email = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "E-Mail",
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

            fr_email = new Frame
            {
                Content = en_email,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_email);

            // Пароль
            lb_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Пароль",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_password);

            en_password = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "**********",
                IsPassword = true,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            fr_password = new Frame
            {
                Content = en_password,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_password);

            // Повторить пароль
            lb_rep_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Повторите пароль",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_rep_password);

            en_rep_password = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "**********",
                IsPassword = true,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            fr_rep_password = new Frame
            {
                Content = en_rep_password,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
            };

            layout.Children.Add(fr_rep_password);

            // CheckBox согласие на обработку
            sl_approve = new StackLayout();
            sl_approve.Orientation = StackOrientation.Horizontal;

            cb_approve = new CheckBox()
            {
                Color = Color.SpringGreen,
                Margin = new Thickness(10, 0, -5, 5),
            };

            sl_approve.Children.Add(cb_approve);

            lb_approve = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Согласие на обработку персональных данных",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 0, 0, 5),
                VerticalOptions = LayoutOptions.Center,
            };

            sl_approve.Children.Add(lb_approve);

            layout.Children.Add(sl_approve);

            // Кнопка создать аккаунт

            bt_next = new Button()
            {
                Text = "Далее",
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

            layout.Children.Add(bt_next);

            // Добавить  
            scrollview = new ScrollView();
            scrollview.Content = layout;
            this.Content = scrollview;
            InitializeComponent();
        }

        public void OnBirthDateTextChanged(object sender, EventArgs e)
        {
            dp_birthdate.TextColor = Color.Black;
        }
    }
}