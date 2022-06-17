using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        #region Инициализация 
        private StackLayout layout;
        private Label lb_nickname_val, lb_breed, lb_breed_val, lb_weight, lb_weight_val, lb_birthdate, lb_birthdate_val;
        private CustomButton bt_edit;
        private Image im_doge;

        #endregion

        [Obsolete]
        public MainPageDetail()
        {
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            im_doge = new Image
            {
                Source = "ben" +
                ".jpg",
                Margin = new Thickness(50, 20, 50, 0),
                BackgroundColor = Color.FromRgb(242, 242, 242),
            };

            layout.Children.Add(im_doge);

            lb_nickname_val = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Стасян",
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                Margin = new Thickness(0, 5, 0, 15),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_nickname_val);

            // Порода собаки
            lb_breed = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Кличка собаки",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };

            layout.Children.Add(lb_breed);

            lb_breed_val = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Стасян",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_breed_val);

            lb_weight = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Вес собаки в кг.",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
                TextDecorations = TextDecorations.Underline
            };

            layout.Children.Add(lb_weight);

            lb_weight_val = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "15.0",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_weight_val);

            lb_birthdate = new Label()
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.Start,
                Text = "Дата рождения собаки",
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
                Text = "09.06.2022",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Дата рождения собаки",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_birthdate_val);

            bt_edit = new CustomButton()
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
                Margin = new Thickness(0, 240, 0, 5),
            };

            layout.Children.Add(bt_edit);

            bt_edit.Clicked += OnEditClicked;

            #endregion

            this.Content = layout;
            InitializeComponent();
        }

        #region Обработка событий

        private async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditDogeProfilePage());

        }

        #endregion
    }
}