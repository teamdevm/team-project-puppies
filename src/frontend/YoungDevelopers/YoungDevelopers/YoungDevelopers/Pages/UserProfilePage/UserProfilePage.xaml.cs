using DogsCompanion.Api.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        #region Инициализация 

        private UserInfo user;
        private StackLayout layout;
        private ScrollView scrollview;
        private Label lb_lastname, lb_lastname_val, lb_firstname, lb_firstname_val, lb_patronymic, lb_patronymic_val, lb_birthdate, lb_birthdate_val, lb_phone, lb_phone_val, lb_email, lb_email_val, lb_empty_field;
        private Button bt_edit, bt_logout;
        private DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;

        #endregion
        public UserProfilePage()
        {
            layout = new StackLayout();
            scrollview = new ScrollView();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            user = DataControl.GetCurrentUserItem();

            #region Элементы страницы

            lb_empty_field = new Label()
            {
                IsVisible = true,
                HeightRequest = 10,
            };
            layout.Children.Add(lb_empty_field);

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
                Text = user.LastName.ToString(),
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
                Text = user.FirstName,
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
                Text = user.MiddleName.ToString(),
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
                Text = user.BirthDate == null ? "" : DateTime.Parse(user.BirthDate.ToString()).ToString("dd.MM.yyyy"),
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
                Text = user.PhoneNumber,
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
                Text = user.Email,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 170),
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
                BackgroundColor = Color.FromRgb(105,233,165),
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
                BackgroundColor = Color.FromRgb(105,233,165),
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
            UpdateFieldsFromServer();
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

        public async void UpdateFieldsFromServer()
        {
            try
            {
                UserInfo updateUser = (UserInfo)await dogsCompanionClient.GetUserInfoAsync();
                DataControl.SetUserInfoItem(updateUser);

                lb_lastname_val.Text = updateUser.LastName.ToString();
                lb_firstname_val.Text = updateUser.FirstName;
                lb_patronymic_val.Text = updateUser.MiddleName.ToString();
                lb_birthdate_val.Text = updateUser.BirthDate == null ? "" : DateTime.Parse(user.BirthDate.ToString()).ToString("dd.MM.yyyy");
                lb_phone_val.Text = updateUser.PhoneNumber;
                lb_email_val.Text = updateUser.Email;

                int downspacing = 0;
                if (lb_lastname_val.Text == "")
                {
                    lb_lastname.IsVisible = false;
                    lb_lastname_val.IsVisible = false;
                    downspacing += 70;
                }
                else
                {
                    lb_lastname.IsVisible = true;
                    lb_lastname_val.IsVisible = true;
                }
                if (lb_patronymic_val.Text == "")
                {
                    lb_patronymic.IsVisible = false;
                    lb_patronymic_val.IsVisible = false;
                    downspacing += 70;
                }
                else
                {
                    lb_patronymic.IsVisible = true;
                    lb_patronymic_val.IsVisible = true;
                }
                if (lb_birthdate_val.Text == "")
                {
                    lb_birthdate.IsVisible = false;
                    lb_birthdate_val.IsVisible = false;
                    downspacing += 70;
                }
                else
                {
                    lb_birthdate.IsVisible = true;
                    lb_birthdate_val.IsVisible = true;
                }

                lb_email_val.Margin = new Thickness(15, 0, 0, 165 + downspacing);
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
            }
        }

        protected override void OnAppearing()
        {
            UpdateFieldsFromServer();
        }
    }
}