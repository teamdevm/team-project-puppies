using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    public class VetClinicShortInfo
    {
        public int ID;
        public string Name, Address, ClockRound;

        public VetClinicShortInfo(int ID, string Name, string Address, string ClockRound)
        {
            this.ID = ID;
            this.Name = Name;
            this.Address = Address;
            this.ClockRound = ClockRound;
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VetClinicsPage : ContentPage
    {
        #region Инициализация
        private StackLayout layout;
        private List<CustomButton> Buttons = new List<CustomButton>();
        private List<VetClinicShortInfo> VetsList = new List<VetClinicShortInfo>()
        {
            new VetClinicShortInfo(0, "Pet Spa", "ул. Пушкина, 66", "+"),
            new VetClinicShortInfo(1, "Артемон и К", "ул. Вильямса, 4А", "-"),
            new VetClinicShortInfo(2, "Love is Dogs", "ул. Мира, 122/1", "-"),
            new VetClinicShortInfo(3, "Модный Енот", "ул. Мира, 3, Этаж 1", "-"),
        };

        #endregion

        public VetClinicsPage()
        {
            Title = "Список ветклиник";
            layout = new StackLayout();
            layout.Orientation = StackOrientation.Vertical;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            Buttons.Add(new CustomButton
            {
                Text = '\n' + "   " + VetsList[0].Name + "\n\n   " + VetsList[0].Address + "\n\n   " + "Круглосуточно  " + VetsList[0].ClockRound + '\n',
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

            Buttons[0].Clicked += OnVetPressed;
            layout.Children.Add(Buttons[0]);

            for (int i = 1; i < VetsList.Count; i++)
            {
                Buttons.Add(new CustomButton
                {
                    Text = '\n' + "   " + VetsList[i].Name + "\n\n   " + VetsList[i].Address + "\n\n   " + "Круглосуточно  " + VetsList[i].ClockRound + '\n',
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

                Buttons[i].Clicked += OnVetPressed;
                layout.Children.Add(Buttons[i]);
            }

            #endregion
            this.Content = layout;
            InitializeComponent();
        }

        #region Обработка событий

        public async void OnVetPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VetClinicInfoPage());
        }


        #endregion
    }
}