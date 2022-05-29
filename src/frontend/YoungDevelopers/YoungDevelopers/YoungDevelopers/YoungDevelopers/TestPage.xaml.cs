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
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            Label header = new Label()
            {
                Text =
                "Sample Text"
            };
            //this.Content = header;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            button1.Text = "Нажато!!!";
        }
    }
}