using System.ComponentModel;
using Xamarin.Forms;
using YoungDevelopers.ViewModels;

namespace YoungDevelopers.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}