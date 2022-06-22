using DogsCompanion.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroomingInfoPage : ContentPage
    {
        #region Инициализация
        public int GroomingId;
        private StackLayout layout;
        private ScrollView scrollview;
        private Label lb_phone, lb_address, lb_site, lb_rating, lb_schedule;
        private GroomerSalon GroomSalon;
        private Button bt_name;
        DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];

        #endregion
        public GroomingInfoPage(int groomingId)
        {
            GroomingId = groomingId;
            GroomSalon = DataControl.GetGroomingItem(GroomingId);
            Title = "Информация о груминг-салоне";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            bt_name = new CustomButton
            {
                
                Text = GroomSalon.Name,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 8,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 370,
                Margin = new Thickness(0, 20, 0, 20),
            };

            layout.Children.Add(bt_name);

            lb_address = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Адрес: " + GroomSalon.Address,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 20, 0, 20),
            };

            layout.Children.Add(lb_address);

            lb_phone = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон: " + GroomSalon.PhoneNumber,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_phone);

            lb_site = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Сайт: " + GroomSalon.Link,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_site);

            lb_rating = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Рейтинг: " + GroomSalon.Rating.ToString() + " ИЗ " + '5',
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_rating);

            lb_schedule = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Расписание",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };

            layout.Children.Add(lb_schedule);

            #endregion


            scrollview = new ScrollView();
            scrollview.Content = layout;
            this.Content = scrollview;
            InitializeComponent();

            UpdateFieldsFromServer();
        }
        #region Обработка событий

        #endregion

        public async void UpdateFieldsFromServer()
        {
            GroomSalon = (GroomerSalon)await dogsCompanionClient.GetGroomerSalonAsync(GroomingId);
            DataControl.SetGroomingItem(GroomSalon);
        }
    }
}