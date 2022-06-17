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
    public partial class ForgetPassPage : ContentPage
    {
        public ForgetPassPage()
        {
            StackLayout layout = new StackLayout();

            Label lb_dogass = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "По зубочистке?",
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                Margin = new Thickness(50, 5, 50, 50),
            };

            layout.Children.Add(lb_dogass);

            Image im_mads = new Image()
            {
                Source = "sexembodiment.jpg",
            };

            layout.Children.Add(im_mads);

            this.Content = layout;
            InitializeComponent();
        }
    }
}