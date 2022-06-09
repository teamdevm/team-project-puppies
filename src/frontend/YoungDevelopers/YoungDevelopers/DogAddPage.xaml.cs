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
    public partial class DogAddPage : ContentPage
    {
        private StackLayout layout;
        private Image im_doge;
        private Frame nickname;

        //SendRegistraion RegFields
        public DogAddPage()
        {
            layout = new StackLayout();


            InitializeComponent();
        }
    }
}