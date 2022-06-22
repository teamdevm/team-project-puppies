using DogsCompanion.Api.Client;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroomingsPage : ContentPage
    {
        #region Инициализация
        private StackLayout layout;
        private ScrollView scrollview;
        private List<GroomerSalon> groomings;
        private List<CustomButton> Buttons = new List<CustomButton>();
        DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];

        #endregion
        [Obsolete]
        public GroomingsPage()
        {
            Title = "Список Груминг-Салонов";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Полученые данных
            groomings = ((Data)App.Current.Properties["storedata"]).VetGroomerSalons;

            #region Элементы страницы

            if (groomings.Count > 0)
            {
                Buttons.Add(new CustomButton
                {
                    ItemId = groomings[0].Id,
                    Text = '\n' + "   " + groomings[0].Name + "\n\n   " + groomings[0].Address + '\n',
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.SpringGreen,
                    CornerRadius = 8,
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontFamily = "Cascadia Code Light",
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 370,
                    Margin = new Thickness(0, 20, 0, 10),
                });

                Buttons[0].Clicked += OnGroomingPressed;
                layout.Children.Add(Buttons[0]);
            }

            if (groomings.Count > 1)
            {
                for (int i = 1; i < groomings.Count; i++)
                {
                    Buttons.Add(new CustomButton
                    {
                        ItemId = groomings[i].Id,
                        Text = '\n' + "   " + groomings[i].Name + "\n\n   " + groomings[i].Address + '\n',
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.SpringGreen,
                        CornerRadius = 8,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = "Cascadia Code Light",
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        WidthRequest = 370,
                        Margin = new Thickness(0, 0, 0, 10),
                    });

                    Buttons[i].Clicked += OnGroomingPressed;
                    layout.Children.Add(Buttons[i]);
                }

            }

            #endregion
            scrollview = new ScrollView();
            scrollview.Content = layout;
            this.Content = scrollview;
            InitializeComponent();

            UpdateFieldsFromServer();
        }

        #region Обработка событий

        public async void OnGroomingPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroomingInfoPage(((CustomButton)sender).ItemId));
            //DisplayAlert("Error", sender., "OK");
        }


        #endregion

        private async void UpdateFieldsFromServer()
        {
            layout = new StackLayout();
            groomings = (List<GroomerSalon>)await dogsCompanionClient.GetGroomerSalonsAsync();
            ((Data)App.Current.Properties["storedata"]).VetGroomerSalons = groomings;

            if (groomings.Count > 0)
            {
                Buttons.Add(new CustomButton
                {
                    ItemId = groomings[0].Id,
                    Text = '\n' + "   " + groomings[0].Name + "\n\n   " + groomings[0].Address + '\n',
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.SpringGreen,
                    CornerRadius = 8,
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontFamily = "Cascadia Code Light",
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 370,
                    Margin = new Thickness(0, 20, 0, 10),
                });

                Buttons[0].Clicked += OnGroomingPressed;
                layout.Children.Add(Buttons[0]);
            }

            if (groomings.Count > 1)
            {
                for (int i = 1; i < groomings.Count; i++)
                {
                    Buttons.Add(new CustomButton
                    {
                        ItemId = groomings[i].Id,
                        Text = '\n' + "   " + groomings[i].Name + "\n\n   " + groomings[i].Address + '\n',
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.SpringGreen,
                        CornerRadius = 8,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = "Cascadia Code Light",
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        WidthRequest = 370,
                        Margin = new Thickness(0, 0, 0, 10),
                    });

                    Buttons[i].Clicked += OnGroomingPressed;
                    layout.Children.Add(Buttons[i]);
                }
            }
            scrollview = new ScrollView();
            scrollview.Content = layout;
            this.Content = scrollview;
        }

        public async void ErrorAlert(string a)
        {
            await DisplayAlert("Error", a, "OK");
        }
    }
}