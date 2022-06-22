using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        public ListView ListView;
        public static string AccountName = "Стасян";

        public MainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class MainPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

            public MainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
                {
                    new MainPageFlyoutMenuItem { Id = 0, Title = "Профиль собаки", TargetType = typeof(MainPageDetail) },
                    new MainPageFlyoutMenuItem { Id = 1, Title = "Профиль пользователя", TargetType = typeof(UserProfilePage) },
                    new MainPageFlyoutMenuItem { Id = 2, Title = "Записаться на процедуры", TargetType = typeof(ProcRegPage) },
                    new MainPageFlyoutMenuItem { Id = 3, Title = "Напоминания", TargetType = typeof(ForgetPassPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}