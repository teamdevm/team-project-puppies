﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        #region Инициализация 

        private StackLayout layout;
        private ScrollView scrollview;
        private Label lb_lastname, lb_lastname_val, lb_firstname, lb_firstname_val, lb_patronymic, lb_patronymic_val, lb_birthdate, lb_birthdate_val, lb_phone, lb_phone_val, lb_email, lb_email_val;
        private Button bt_edit, bt_logout;

        #endregion
        public UserProfilePage()
        {
            layout = new StackLayout();
            scrollview = new ScrollView();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            // Фамилия
            lb_lastname = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Фамилия",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_lastname);

            lb_lastname_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Пупкин",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_lastname_val);

            // Имя
            lb_firstname = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Имя",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_firstname);

            lb_firstname_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Вася",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_firstname_val);

            // Отчество
            lb_patronymic = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Отчество",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_patronymic);

            lb_patronymic_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Станиславович",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_patronymic_val);

            // Дата рождения
            lb_birthdate = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Дата рождения",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_birthdate);

            lb_birthdate_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "16.06.22",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_birthdate_val);

            // Телефон
            lb_phone = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_phone);

            lb_phone_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "+7 (999) 999-99-99",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_phone_val);

            // E-mail
            lb_email = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "E-Mail",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };
            layout.Children.Add(lb_email);

            lb_email_val = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "pupkin@mail.ru",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_email_val);

            // Кнопка редактировать
            bt_edit = new Button()
            {
                Text = "Редактировать",
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

            layout.Children.Add(bt_edit);
            bt_edit.Clicked += OnEditClicked;

            // Кнопка редактировать
            bt_logout = new Button()
            {
                Text = "Выйти из аккаунта",
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

            layout.Children.Add(bt_logout);
            bt_logout.Clicked += OnLogoutClicked;

            #endregion

            scrollview.Content = layout;
            this.Content = scrollview;
            InitializeComponent();
        }

        #region Обработка событий

        public async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserProfilePage());
        }

        public void OnLogoutClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        #endregion
    }
}