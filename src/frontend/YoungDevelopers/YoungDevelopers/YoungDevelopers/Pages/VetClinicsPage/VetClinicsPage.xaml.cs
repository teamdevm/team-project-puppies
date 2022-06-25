using DogsCompanion.Api.Client;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VetClinicsPage : ContentPage
    {
        #region Инициализация
        private StackLayout layout;
        private ScrollView scrollview;
        private List<CustomButton> Buttons = new List<CustomButton>();
        private List<VetClinic> Clinics;
        DogsCompanionClient dogsCompanionClient = (DogsCompanionClient)App.Current.Properties["dogsCompanionClient"];

        #endregion

        [Obsolete]
        public VetClinicsPage()
        {
            
            Title = "Список ветклиник";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Полученые данных
            Clinics = ((Data)App.Current.Properties["storedata"]).VetClinics;

            #region Элементы страницы

            if (Clinics.Count > 0)
            {
                Buttons.Add(new CustomButton
                {
                    ItemId = Clinics[0].Id,
                    Text = '\n' + "   " + Clinics[0].Name + "\n\n   " + Clinics[0].Address + "\n\n   " + "Круглосуточно  " + DataControl.GetEmoji(Clinics[0].IsAllDay) + '\n',
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.FromRgb(105,233,165),
                    CornerRadius = 8,
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontFamily = "Cascadia Code Light",
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 370,
                    Margin = new Thickness(0, 20, 0, 10),
                });

                Buttons[0].Clicked += OnVetPressed;
                layout.Children.Add(Buttons[0]);

                if (Clinics.Count > 1)
                {
                    for (int i = 1; i < Clinics.Count; i++)
                    {
                        Buttons.Add(new CustomButton
                        {
                            ItemId = Clinics[i].Id,
                            Text = '\n' + "   " + Clinics[i].Name + "\n\n   " + Clinics[i].Address + "\n\n   " + "Круглосуточно  " + DataControl.GetEmoji(Clinics[i].IsAllDay) + '\n',
                            FontAttributes = FontAttributes.Bold,
                            BackgroundColor = Color.FromRgb(105,233,165),
                            CornerRadius = 8,
                            HorizontalTextAlignment = TextAlignment.Start,
                            FontFamily = "Cascadia Code Light",
                            TextColor = Color.White,
                            HorizontalOptions = LayoutOptions.Center,
                            WidthRequest = 370,
                            Margin = new Thickness(0, 0, 0, 10),
                        });

                        Buttons[i].Clicked += OnVetPressed;
                        layout.Children.Add(Buttons[i]);
                    }
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

        public async void OnVetPressed(object sender, EventArgs e)
        {
            int id = ((CustomButton)sender).ItemId;
            await Navigation.PushAsync(new VetClinicInfoPage(id));
        }

        #endregion

        private async void UpdateFieldsFromServer()
        {
            try
            {
                layout = new StackLayout();
                Buttons = new List<CustomButton>();
                Clinics = (List<VetClinic>)await dogsCompanionClient.GetVetClinicsAsync();
                ((Data)App.Current.Properties["storedata"]).VetClinics = Clinics;

                if (Clinics.Count > 0)
                {
                    Buttons.Add(new CustomButton
                    {
                        ItemId = Clinics[0].Id,
                        Text = '\n' + "   " + Clinics[0].Name + "\n\n   " + Clinics[0].Address + "\n\n   " + "Круглосуточно  " + DataControl.GetEmoji(Clinics[0].IsAllDay) + '\n',
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.FromRgb(105, 233, 165),
                        CornerRadius = 8,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = "Cascadia Code Light",
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.Center,
                        WidthRequest = 370,
                        Margin = new Thickness(0, 20, 0, 10),
                    });

                    Buttons[0].Clicked += OnVetPressed;
                    layout.Children.Add(Buttons[0]);

                    if (Clinics.Count > 1)
                    {
                        for (int i = 1; i < Clinics.Count; i++)
                        {
                            Buttons.Add(new CustomButton
                            {
                                ItemId = Clinics[i].Id,
                                Text = '\n' + "   " + Clinics[i].Name + "\n\n   " + Clinics[i].Address + "\n\n   " + "Круглосуточно  " + DataControl.GetEmoji(Clinics[i].IsAllDay) + '\n',
                                FontAttributes = FontAttributes.Bold,
                                BackgroundColor = Color.FromRgb(105, 233, 165),
                                CornerRadius = 8,
                                HorizontalTextAlignment = TextAlignment.Start,
                                FontFamily = "Cascadia Code Light",
                                TextColor = Color.White,
                                HorizontalOptions = LayoutOptions.Center,
                                WidthRequest = 370,
                                Margin = new Thickness(0, 0, 0, 10),
                            });

                            Buttons[i].Clicked += OnVetPressed;
                            layout.Children.Add(Buttons[i]);
                        }
                    }
                }
                scrollview = new ScrollView();
                scrollview.Content = layout;
                this.Content = scrollview;
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка", "Сервис недоступен", "OK");
            }
        }
    }
}