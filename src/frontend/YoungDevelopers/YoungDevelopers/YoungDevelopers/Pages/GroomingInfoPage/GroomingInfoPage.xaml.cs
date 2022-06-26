using DogsCompanion.Api.Client;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroomingInfoPage : ContentPage
    {
        #region Инициализация
        public int GroomingId;
        private bool AllDay;
        private StackLayout layout;
        private ScrollView scrollview;
        private Label lb_phone, lb_address, lb_site, lb_rating, lb_schedule,
            lb_monday, lb_tuesday, lb_wednesday, lb_thursday, lb_friday, lb_saturday, lb_sunday;
        private GroomerSalon GroomSalon;
        private Button bt_name;
        DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;

        #endregion
        public GroomingInfoPage(int groomingId)
        {
            GroomingId = groomingId;
            GroomSalon = DataControl.GetGroomingItem(GroomingId);
            AllDay = false;
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
                BackgroundColor = Color.FromRgb(105,233,165),
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
                Margin = new Thickness(15, 20, 0, 15),
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
                Margin = new Thickness(15, 0, 0, 0),
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
                Margin = new Thickness(15, 0, 0, 0),
                TextDecorations = TextDecorations.Underline,
            };

            System.Windows.Input.ICommand command = new Command<string>((url) =>
            {
                try
                {
                    string formttedUrl = url;
                    if (!url.StartsWith("http"))
                    {
                        formttedUrl = $"http://{url}";
                    }
                    Xamarin.Essentials.Launcher.OpenAsync(formttedUrl);
                }
                catch (Exception)
                {
                }
            });

            lb_site.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = command,
                CommandParameter = GroomSalon.Link
            });

            layout.Children.Add(lb_site);

            lb_rating = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Рейтинг: " + (GroomSalon.Rating == 0 ? "нет оценок" : $"{GroomSalon.Rating} из 5"),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 10),
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

            lb_monday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "ПН", GroomSalon.OpeningHours.First(s => s.Day == Day._1).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_monday);

            lb_tuesday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "ВТ", GroomSalon.OpeningHours.First(s => s.Day == Day._2).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_tuesday);

            lb_wednesday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "СР", GroomSalon.OpeningHours.First(s => s.Day == Day._3).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_wednesday);

            lb_thursday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "ЧТ", GroomSalon.OpeningHours.First(s => s.Day == Day._4).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_thursday);

            lb_friday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "ПТ", GroomSalon.OpeningHours.First(s => s.Day == Day._5).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_friday);

            lb_saturday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "СБ", GroomSalon.OpeningHours.First(s => s.Day == Day._6).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 0),
            };

            layout.Children.Add(lb_saturday);

            lb_sunday = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = DataControl.GetHoursString(AllDay, "ВС", GroomSalon.OpeningHours.First(s => s.Day == Day._7).Periods),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                Margin = new Thickness(15, 0, 0, 0),
            };

            layout.Children.Add(lb_sunday);
            #endregion


            scrollview = new ScrollView();
            scrollview.Content = layout;
            this.Content = scrollview;
            InitializeComponent();

            UpdateFieldsFromServer();
        }

        public async void UpdateFieldsFromServer()
        {
            try
            {
                GroomSalon = (GroomerSalon)await dogsCompanionClient.GetGroomerSalonAsync(GroomingId);
                DataControl.SetGroomingItem(GroomSalon);

                bt_name.Text = GroomSalon.Name;
                lb_address.Text = "Адрес: " + GroomSalon.Address;
                lb_phone.Text = "Телефон: " + GroomSalon.PhoneNumber;
                lb_site.Text = "Сайт: " + GroomSalon.Link;
                lb_rating.Text = "Рейтинг: " + (GroomSalon.Rating == 0 ? "нет оценок" : $"{GroomSalon.Rating} из 5");
                lb_monday.Text = DataControl.GetHoursString(AllDay, "ПН", GroomSalon.OpeningHours.First(s => s.Day == Day._1).Periods);
                lb_tuesday.Text = DataControl.GetHoursString(AllDay, "ВТ", GroomSalon.OpeningHours.First(s => s.Day == Day._2).Periods);
                lb_wednesday.Text = DataControl.GetHoursString(AllDay, "СР", GroomSalon.OpeningHours.First(s => s.Day == Day._3).Periods);
                lb_thursday.Text = DataControl.GetHoursString(AllDay, "ЧТ", GroomSalon.OpeningHours.First(s => s.Day == Day._4).Periods);
                lb_friday.Text = DataControl.GetHoursString(AllDay, "ПТ", GroomSalon.OpeningHours.First(s => s.Day == Day._5).Periods);
                lb_saturday.Text = DataControl.GetHoursString(AllDay, "СБ", GroomSalon.OpeningHours.First(s => s.Day == Day._6).Periods);
                lb_sunday.Text = DataControl.GetHoursString(AllDay, "ВС", GroomSalon.OpeningHours.First(s => s.Day == Day._7).Periods);
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
            }
        }
    }
}