using DogsCompanion.Api.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VetClinicInfoPage : ContentPage
    {
        #region Инициализация
        public int VetClinicID;
        private bool AllDay;
        private StackLayout layout;
        private ScrollView scrollview;
        private Label lb_phone, lb_address, lb_site, lb_rating, lb_schedule, lb_clockar,
            lb_monday, lb_tuesday, lb_wednesday, lb_thursday, lb_friday, lb_saturday, lb_sunday;
        private VetClinic vetClinic;
        private Button bt_name;
        DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];

        #endregion
        public VetClinicInfoPage(int vetClinicID)
        {
            VetClinicID = vetClinicID;
            vetClinic = DataControl.GetVetClinicItem(VetClinicID);
            AllDay = vetClinic.IsAllDay;
            Title = "Информация о ветклинике";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            bt_name = new CustomButton
            {

                Text = vetClinic.Name,
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
                Text = "Адрес: " + vetClinic.Address,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 20, 0, 0),
            };

            layout.Children.Add(lb_address);

            lb_clockar = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "КРУГЛОСУТОЧНО " + DataControl.GetEmoji(vetClinic.IsAllDay),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 15),
            };

            layout.Children.Add(lb_clockar);

            lb_phone = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон: " + vetClinic.PhoneNumber,
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
                Text = "Сайт: " + vetClinic.Link,
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
                CommandParameter = vetClinic.Link
            });

            layout.Children.Add(lb_site);

            lb_rating = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Рейтинг: " + vetClinic.Rating.ToString() + " ИЗ " + '5',
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
                Text = DataControl.GetHoursString(AllDay, "ПН", vetClinic.OpeningHours.First(s => s.Day == Day._1).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "ВТ", vetClinic.OpeningHours.First(s => s.Day == Day._2).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "СР", vetClinic.OpeningHours.First(s => s.Day == Day._3).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "ЧТ", vetClinic.OpeningHours.First(s => s.Day == Day._4).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "ПТ", vetClinic.OpeningHours.First(s => s.Day == Day._5).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "СБ", vetClinic.OpeningHours.First(s => s.Day == Day._6).Periods),
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
                Text = DataControl.GetHoursString(AllDay, "ВС", vetClinic.OpeningHours.First(s => s.Day == Day._7).Periods),
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
                vetClinic = (VetClinic)await dogsCompanionClient.GetVetClinicAsync(VetClinicID);
                DataControl.SetVetclinicItem(vetClinic);

                bt_name.Text = vetClinic.Name;
                lb_address.Text = "Адрес: " + vetClinic.Address;
                lb_clockar.Text = "КРУГЛОСУТОЧНО " + DataControl.GetEmoji(vetClinic.IsAllDay);
                lb_phone.Text = "Телефон: " + vetClinic.PhoneNumber;
                lb_site.Text = "Сайт: " + vetClinic.Link;
                lb_rating.Text = "Рейтинг: " + vetClinic.Rating.ToString() + " ИЗ " + '5';
                lb_monday.Text = DataControl.GetHoursString(AllDay, "ПН", vetClinic.OpeningHours.First(s => s.Day == Day._1).Periods);
                lb_tuesday.Text = DataControl.GetHoursString(AllDay, "ВТ", vetClinic.OpeningHours.First(s => s.Day == Day._2).Periods);
                lb_wednesday.Text = DataControl.GetHoursString(AllDay, "СР", vetClinic.OpeningHours.First(s => s.Day == Day._3).Periods);
                lb_thursday.Text = DataControl.GetHoursString(AllDay, "ЧТ", vetClinic.OpeningHours.First(s => s.Day == Day._4).Periods);
                lb_friday.Text = DataControl.GetHoursString(AllDay, "ПТ", vetClinic.OpeningHours.First(s => s.Day == Day._5).Periods);
                lb_saturday.Text = DataControl.GetHoursString(AllDay, "СБ", vetClinic.OpeningHours.First(s => s.Day == Day._6).Periods);
                lb_sunday.Text = DataControl.GetHoursString(AllDay, "ВС", vetClinic.OpeningHours.First(s => s.Day == Day._7).Periods);
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
            }
        }
    }
}