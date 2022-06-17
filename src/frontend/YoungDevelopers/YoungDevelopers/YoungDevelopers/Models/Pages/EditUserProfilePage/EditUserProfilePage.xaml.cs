using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoungDevelopers
{

    public class SaveChanges
    {
        public string lastname = "",
            firstname = "",
            patronymic = "",
            birthdate = "";
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserProfilePage : ContentPage
    {
        #region Инициализация 

        private bool hasDate = false;
        private ScrollView scrollview;
        private StackLayout layout, sl_approve;
        private Label lb_musthave, lb_lastname, lb_firstname, lb_patronymic, lb_birthdate, lb_lastname_er, lb_firstname_er, lb_patronymic_er, lb_conc_pass_er, lb_main_fields;
        private ControlEntry en_lastname, en_firstname, en_patronymic;
        private Frame fr_lastname, fr_firstname, fr_patronymic, fr_birthdate;
        private Button bt_save, bt_editemail, bt_editphone, bt_editpass;
        private DatePickerControl dp_birthdate;
        private SaveChanges savechanges;
        private Regex
            re_lastname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$");

        #endregion

        public EditUserProfilePage()
        {
            Title = "Редактирование профиля пользователя";
            layout = new StackLayout();
            scrollview = new ScrollView();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            #region Элементы страницы

            // Label обязательные поля
            lb_musthave = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                Text = "* Обязательное поле",
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(15, 5, 0, 15),
                FontAttributes = FontAttributes.Bold,
            };

            layout.Children.Add(lb_musthave);

            // Фамилия
            lb_lastname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Фамилия",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_lastname);

            en_lastname = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "Иванов",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_lastname.TextChanged += OnLastNameTextChanged;
            en_lastname.Unfocused += OnLastNameUnfocused;
            en_lastname.Focused += OnLastNameFocused;

            fr_lastname = new Frame
            {
                Content = en_lastname,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                BorderColor = Color.White,
                HasShadow = true,
            };

            layout.Children.Add(fr_lastname);

            // Ошибка при вводе фамилии
            lb_lastname_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат фамилии",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_lastname_er);

            // Имя
            lb_firstname = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Имя *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_firstname);

            en_firstname = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "Иван",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_firstname.TextChanged += OnFirstNameTextChanged;
            en_firstname.Unfocused += OnFirstNameUnfocused;
            en_firstname.Focused += OnFirstNameFocused;

            fr_firstname = new Frame
            {
                Content = en_firstname,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_firstname);

            // Ошибка при вводе имени
            lb_firstname_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат имени",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_firstname_er);

            // Отчество
            lb_patronymic = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Отчество",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_patronymic);

            en_patronymic = new ControlEntry()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "Иванович",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_patronymic.TextChanged += OnPatronymicTextChanged;
            en_patronymic.Unfocused += OnPatronymicUnfocused;
            en_patronymic.Focused += OnPatronymicFocused;

            fr_patronymic = new Frame
            {
                Content = en_patronymic,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_patronymic);

            // Ошибка при вводе отчества
            lb_patronymic_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат отчества",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_patronymic_er);

            // Дата рождения
            lb_birthdate = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Дата рождения",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_birthdate);

            dp_birthdate = new DatePickerControl()
            {
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-5, -15, 0, -12),
                TextColor = Color.Gray,
                Format = "dd.MM.yyyy",
            };


            dp_birthdate.DateSelected += OnDateSelected;

            fr_birthdate = new Frame
            {
                Content = dp_birthdate,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_birthdate);

            // Кнопка Сохранить изменения
            bt_save = new Button()
            {
                Text = "Сохранить изменения",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_save);
            bt_save.Clicked += OnSaveClicked;

            // Не заполнены обязательные поля
            lb_main_fields = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Не заполнены обязательные поля",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            bt_editemail = new Button()
            {
                Text = "Редактировать почту",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_editemail);
            bt_editemail.Clicked += OnEditEmailClicked;

            bt_editphone = new Button()
            {
                Text = "Редактировать телефон",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_editphone);
            bt_editphone.Clicked += OnEditPhoneClicked;

            bt_editpass = new Button()
            {
                Text = "Редактировать пароль",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.SpringGreen,
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_editpass);
            bt_editpass.Clicked += OnEditPassClicked;

            #endregion

            scrollview.Content = layout;
            scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
            this.Content = scrollview;
            InitializeComponent();
        }

         #region Обработка событий

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (lb_conc_pass_er.IsVisible == false && fr_firstname.BorderColor == Color.White)
                {
                    lb_main_fields.IsVisible = false;
                }
            }

        }

        public void OnBirthDateTextChanged(object sender, EventArgs e)
        {
            dp_birthdate.TextColor = Color.Black;
        }

        public void OnLastNameTextChanged(object sender, EventArgs e)
        {
            fr_lastname.BorderColor = Color.White;
            lb_lastname_er.IsVisible = false;
            fr_lastname.BackgroundColor = Color.White;
            en_lastname.BackgroundColor = Color.White;
            en_lastname.TextColor = Color.Black;
        }

        public void OnLastNameFocused(object sender, EventArgs e)
        {
            fr_lastname.BorderColor = Color.SpringGreen;
        }

        public void OnLastNameUnfocused(object sender, EventArgs e)
        {
            fr_lastname.BorderColor = Color.White;
            if (en_lastname.Text == "" || en_lastname.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_lastname.Text);
                if (!match.Success)
                {
                    fr_lastname.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_lastname_er.IsVisible = true;
                    fr_lastname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_lastname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_lastname.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_lastname.BorderColor = Color.White;
                    lb_lastname_er.IsVisible = false;
                    fr_lastname.BackgroundColor = Color.White;
                    en_lastname.BackgroundColor = Color.White;
                    en_lastname.TextColor = Color.Black;
                }
            }
        }

        public void OnFirstNameTextChanged(object sender, EventArgs e)
        {
            fr_firstname.BorderColor = Color.White;
            lb_firstname_er.IsVisible = false;
            fr_firstname.BackgroundColor = Color.White;
            en_firstname.BackgroundColor = Color.White;
            en_firstname.TextColor = Color.Black;
        }

        public void OnFirstNameFocused(object sender, EventArgs e)
        {
            fr_firstname.BorderColor = Color.SpringGreen;
        }
        public void OnFirstNameUnfocused(object sender, EventArgs e)
        {
            fr_firstname.BorderColor = Color.White;
            if (en_firstname.Text == "" || en_firstname.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_firstname.Text);
                if (!match.Success)
                {
                    fr_firstname.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_firstname_er.IsVisible = true;
                    fr_firstname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_firstname.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_firstname.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_firstname.BorderColor = Color.White;
                    lb_firstname_er.IsVisible = false;
                    fr_firstname.BackgroundColor = Color.White;
                    en_firstname.BackgroundColor = Color.White;
                    en_firstname.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        public void OnPatronymicTextChanged(object sender, EventArgs e)
        {
            fr_patronymic.BorderColor = Color.White;
            lb_patronymic_er.IsVisible = false;
            fr_patronymic.BackgroundColor = Color.White;
            en_patronymic.BackgroundColor = Color.White;
            en_patronymic.TextColor = Color.Black;
        }

        public void OnPatronymicFocused(object sender, EventArgs e)
        {
            fr_patronymic.BorderColor = Color.SpringGreen;
        }

        public void OnPatronymicUnfocused(object sender, EventArgs e)
        {
            fr_patronymic.BorderColor = Color.White;
            if (en_patronymic.Text == "" || en_patronymic.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_patronymic.Text);
                if (!match.Success)
                {
                    fr_patronymic.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_patronymic_er.IsVisible = true;
                    fr_patronymic.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_patronymic.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_patronymic.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_patronymic.BorderColor = Color.White;
                    lb_patronymic_er.IsVisible = false;
                    fr_patronymic.BackgroundColor = Color.White;
                    en_patronymic.BackgroundColor = Color.White;
                    en_patronymic.TextColor = Color.Black;
                }
            }
        }        

        public void OnApproveCheckedChanged(object sender, EventArgs e)
        {
            CheckRecField();
        }

        public void OnDateSelected(object sender, EventArgs e)
        {
            hasDate = true;
        }

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            //if (fr_lastname.BackgroundColor == Color.FromRgb(194, 85, 85) || fr_firstname.BackgroundColor == Color.FromRgb(194, 85, 85) || fr_patronymic.BackgroundColor == Color.FromRgb(194, 85, 85) || fr_birthdate.BackgroundColor == Color.FromRgb(194, 85, 85)
            //     || lb_conc_pass_er.IsVisible == true || lb_main_fields.IsVisible == true)
            //{
            //    lb_main_fields.IsVisible = true;
            //}
            //else if (en_lastname.Text == "" || en_lastname.Text == null)
            //{
            //    lb_main_fields.IsVisible = true;
            //}
            //else
            //{
            //    lb_main_fields.IsVisible = false;
            //    savechanges = new SaveChanges();
            //    // if not empty
            //    savechanges.lastname = en_lastname.Text;
            //    savechanges.firstname = en_firstname.Text;
            //    savechanges.patronymic = en_patronymic.Text;
            //    if (hasDate)
            //    {
            //        savechanges.birthdate = dp_birthdate.Date.ToString("dd.MM.yyyy");
            //    }

            //    await Navigation.PopAsync();

            //    // Отправить Васе + ошибку сделать
            //    //lb_phone_exists
            //    //lb_email_exists
            //}
        }

        public async void OnEditEmailClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditEmailPage());
        }

        public async void OnEditPhoneClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPhonePage());
        }

        public async void OnEditPassClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPassPage());
        }

        #endregion
    }
}