using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using DogsCompanion.Api.Client;

namespace YoungDevelopers
{
    public class SendRegistraion
    {
        public string lastname = "",
            firstname = "",
            patronymic = "",
            phone = "",
            email = "",
            password = "";
        public System.DateTimeOffset? birthdate = null;
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        #region Инициализация 
        private bool hasDate = false;
        private ScrollView scrollview;
        private StackLayout main, layout, sl_approve;
        private Label lb_musthave, lb_lastname, lb_firstname, lb_patronymic, lb_birthdate, lb_phone, lb_email, lb_password, lb_rep_password, lb_approve,
             lb_lastname_er, lb_firstname_er, lb_patronymic_er, lb_phone_er, lb_email_er, lb_password_er, lb_rep_password_er, lb_conc_pass_er, lb_main_fields, lb_reg_er;
        private ControlEntry en_lastname, en_firstname, en_patronymic, en_email, en_password, en_rep_password;
        private Frame fr_lastname, fr_firstname, fr_patronymic, fr_birthdate, fr_phone, fr_email, fr_password, fr_rep_password;
        private Button bt_next;
        private DatePickerControl dp_birthdate;
        private MaskedEntry me_phone;
        private CheckBox cb_approve;
        private SendRegistraion sendreg;
        private Regex 
            re_lastname = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$"),
            re_email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"), 
            re_password = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$");

        #endregion
        public RegistrationPage()
        {
            this.Title = "Регистрация пользователя";
            layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };
            scrollview = new ScrollView();
            main = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };
            layout.Orientation = StackOrientation.Vertical;
            //layout.VerticalOptions = LayoutOptions.FillAndExpand;
            scrollview.Orientation = ScrollOrientation.Vertical;
            //scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
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
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder  = "Иванов",
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
                Text = "",
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
                Text = "",
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
                Date = DateTime.Now,
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

            // Телефон
            lb_phone = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Телефон *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_phone);

            me_phone = new MaskedEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Mask = "+7 (XXX) XXX-XX-XX",
                Placeholder = "+7 (XXX) XXX-XX-XX",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                Background = null,
                BackgroundColor = Color.White,
                Keyboard = Keyboard.Telephone,
                VerticalTextAlignment = TextAlignment.Center
            };

            me_phone.TextChanged += OnPhoneTextChanged;
            me_phone.Unfocused += OnPhoneUnfocused;
            me_phone.Focused += OnPhoneFocusedChanged;

            fr_phone = new Frame
            {
                Content = me_phone,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };


            layout.Children.Add(fr_phone);

            // Неправильный формат телефона
            lb_phone_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат телефона",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_phone_er);

            // E-mail
            lb_email = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "E-Mail *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_email);

            en_email = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "ivanivanov@mail.ru",
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                Keyboard = Keyboard.Email,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_email.TextChanged += OnEmailTextChanged;
            en_email.Unfocused += OnEmailUnfocused;
            en_email.Focused += OnEmailFocused;

            fr_email = new Frame
            {
                Content = en_email,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_email);

            // Ошибка при вводе почты
            lb_email_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат почты",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_email_er);

            // Пароль
            lb_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Пароль *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_password);

            en_password = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "**********",
                IsPassword = true,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_password.TextChanged += OnPasswordTextChanged;
            en_password.Unfocused += OnPasswordUnfocused;
            en_password.Focused += OnPasswordFocused;

            fr_password = new Frame
            {
                Content = en_password,
                CornerRadius = 10,
                HeightRequest = 6,
                BorderColor = Color.White,
                IsClippedToBounds = true,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_password);

            // Ошибка при вводе пароля
            lb_password_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Пароль должен содержать хотя бы одну цифру, латинскую букву в нижнем регистре, латинскую букву в верхнем регистре и спецсимвол",
                Margin = new Thickness(15, -5, 10, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_password_er);

            // Повторить пароль
            lb_rep_password = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Повторите пароль *",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(15, 0, 0, 5),
            };
            layout.Children.Add(lb_rep_password);

            en_rep_password = new ControlEntry()
            {
                Text = "",
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-10, -15, 0, -17.5),
                Placeholder = "**********",
                IsPassword = true,
                MyTintColor = Color.Transparent,
                MyHighlightColor = Color.Gray,
                BackgroundColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            en_rep_password.TextChanged += OnRepPasswordTextChanged;
            en_rep_password.Unfocused += OnRepPasswordUnfocused;
            en_rep_password.Focused += OnRepPasswordFocused;

            fr_rep_password = new Frame
            {
                Content = en_rep_password,
                CornerRadius = 10,
                HeightRequest = 6,
                IsClippedToBounds = true,
                BorderColor = Color.White,
                Margin = new Thickness(10, 0, 10, 10),
                HasShadow = true,
            };

            layout.Children.Add(fr_rep_password);

            // Ошибка при вводе пароля
            lb_rep_password_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Неправильный формат пароля",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_rep_password_er);

            // Ошибка несовпадения
            lb_conc_pass_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Пароли не совпадают",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_conc_pass_er);

            // CheckBox согласие на обработку
            sl_approve = new StackLayout();
            sl_approve.Orientation = StackOrientation.Horizontal;

            cb_approve = new CheckBox()
            {
                Color = Color.SpringGreen,
                Margin = new Thickness(10, 0, -5, 5),
            };

            sl_approve.Children.Add(cb_approve);

            cb_approve.CheckedChanged += OnApproveCheckedChanged;

            lb_approve = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Согласие на обработку персональных данных",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontFamily = "Cascadia Code Light",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 0, 0, 5),
                VerticalOptions = LayoutOptions.Center,
            };

            sl_approve.Children.Add(lb_approve);

            layout.Children.Add(sl_approve);

            // Кнопка Далее

            bt_next = new Button()
            {
                Text = "Далее",
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

            layout.Children.Add(bt_next);
            bt_next.Clicked += OnNextPressed;

            // Не заполнены обязательные поля
            lb_main_fields = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Не заполнены обязательные поля",
                Margin = new Thickness(15, -10, 0, 5),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_main_fields);

            #endregion
            
            scrollview.Content = layout;          
            scrollview.VerticalOptions = LayoutOptions.FillAndExpand;
            main.Children.Add(scrollview);
            this.Content = main;
            InitializeComponent();
        }

        #region Обработка событий

        public async void OnNextPressed(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new DogAddPage());
            //await DisplayAlert("Error", "cock", "OK");
            lb_main_fields.IsVisible = false;
            if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy") && en_lastname.Text == "" && en_firstname.Text == "" && en_patronymic.Text == "" && me_phone.Text == "" && en_email.Text == "" && en_password.Text == "" && en_rep_password.Text == "" && cb_approve.IsChecked == false)
            {
                return;

            }
            else
            {
                if (fr_lastname.BorderColor == Color.FromRgb(194, 85, 85) || fr_firstname.BorderColor == Color.FromRgb(194, 85, 85) || fr_patronymic.BorderColor == Color.FromRgb(194, 85, 85) || fr_birthdate.BorderColor == Color.FromRgb(194, 85, 85)
                || fr_phone.BorderColor == Color.FromRgb(194, 85, 85) || fr_email.BorderColor == Color.FromRgb(194, 85, 85) || fr_password.BorderColor == Color.FromRgb(194, 85, 85)
                || fr_rep_password.BorderColor == Color.FromRgb(194, 85, 85) || lb_conc_pass_er.IsVisible == true)
                {
                    return;
                }
                else
                {
                    if (en_firstname.Text == "" || me_phone.Text == "" || en_email.Text == "" || en_password.Text == "" || en_rep_password.Text == "" || cb_approve.IsChecked == false)
                    {
                        lb_main_fields.IsVisible = true;
                        return;
                    }
                    //RegisterUserAsync
                    sendreg = new SendRegistraion();
                    sendreg.email = en_email.Text;
                    sendreg.password = en_password.Text;
                    sendreg.phone = me_phone.Text;
                    sendreg.firstname = en_firstname.Text;
                    sendreg.lastname = en_lastname.Text;
                    sendreg.patronymic = en_patronymic.Text;
                    if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy"))
                    {
                        sendreg.birthdate = null;
                    }
                    else
                    {
                        sendreg.birthdate = dp_birthdate.Date;
                    }

                    await Navigation.PushAsync(new DogAddPage(sendreg));
                }
            }
        }

        public void CheckRecField()
        {
            if (lb_main_fields.IsVisible == true)
            {
                if (me_phone.Text != "" && en_firstname.Text != "" && en_email.Text != "" && en_password.Text != "" && en_rep_password.Text != "" && lb_conc_pass_er.IsVisible == false && fr_firstname.BorderColor == Color.White && fr_phone.BorderColor == Color.White && fr_email.BorderColor == Color.White && fr_password.BorderColor == Color.White && fr_rep_password.BorderColor == Color.White && cb_approve.IsChecked == true)
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

        public void OnPhoneTextChanged(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.White;
            lb_phone_er.IsVisible = false;
            fr_phone.BackgroundColor = Color.White;
            me_phone.BackgroundColor = Color.White;
            me_phone.TextColor = Color.Black;
        }

        public void OnPhoneFocusedChanged(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.SpringGreen;
        }

        public void OnPhoneUnfocused(object sender, EventArgs e)
        {
            fr_phone.BorderColor = Color.White;
            if (me_phone.Text == "" || me_phone.Text == null) return;
            else
            {
                if (me_phone.Text.Length != 18)
                {
                    fr_phone.BorderColor = Color.FromRgb(194, 85, 85);
                    lb_phone_er.IsVisible = true;
                    fr_phone.BackgroundColor = Color.FromRgb(255, 187, 187);
                    me_phone.BackgroundColor = Color.FromRgb(255, 187, 187);
                    me_phone.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    fr_phone.BorderColor = Color.White;
                    lb_phone_er.IsVisible = false;
                    fr_phone.BackgroundColor = Color.White;
                    me_phone.BackgroundColor = Color.White;
                    me_phone.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        public void OnEmailTextChanged(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.White;
            lb_email_er.IsVisible = false;
            fr_email.BackgroundColor = Color.White;
            en_email.BackgroundColor = Color.White;
            en_email.TextColor = Color.Black;
        }

        public void OnEmailFocused(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.SpringGreen;
        }

        public void OnEmailUnfocused(object sender, EventArgs e)
        {
            fr_email.BorderColor = Color.White;
            if (en_email.Text == "" || en_email.Text == null) return;
            else
            {
                Match match = re_email.Match(en_email.Text);
                if (!match.Success)
                {
                    lb_email_er.IsVisible = true;
                    fr_email.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_email.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_email.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_email.TextColor = Color.FromRgb(194, 85, 85);
                }
                else
                {
                    lb_email_er.IsVisible = false;
                    fr_email.BorderColor = Color.White;
                    fr_email.BackgroundColor = Color.White;
                    en_email.BackgroundColor = Color.White;
                    en_email.TextColor = Color.Black;
                    CheckRecField();
                }
            }
        }

        public void OnPasswordTextChanged(object sender, EventArgs e)
        {
            lb_password_er.IsVisible = false;
            fr_password.BorderColor = Color.White;
            fr_password.BackgroundColor = Color.White;
            en_password.BackgroundColor = Color.White;
            en_password.TextColor = Color.Black;

            if (lb_conc_pass_er.IsVisible == true && fr_rep_password.BorderColor == Color.White && en_rep_password.Text != "" && en_rep_password.Text != null)
            {
                if (en_password.Text == en_rep_password.Text)
                {
                    lb_conc_pass_er.IsVisible = false;
                }
            }
        }

        public void OnPasswordFocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.SpringGreen;
            lb_conc_pass_er.IsVisible = false;
        }

        public void OnPasswordUnfocused(object sender, EventArgs e)
        {
            fr_password.BorderColor = Color.White;
            if (en_password.Text == "" || en_password.Text == null) return;
            else
            {
                Match match = re_password.Match(en_password.Text);
                if (!match.Success)
                {
                    lb_password_er.IsVisible = true;
                    fr_password.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_password.TextColor = Color.FromRgb(194, 85, 85);
                    //layout.HeightRequest = 1000;
                    this.IsVisible = false;
                    this.IsVisible = true;
                }
                else if (en_rep_password.Text != en_password.Text && en_rep_password.Text != null && en_rep_password.Text != "")
                {
                    lb_conc_pass_er.IsVisible = true;
                }
                else
                {
                    lb_password_er.IsVisible = false;
                    fr_password.BorderColor = Color.White;
                    fr_password.BackgroundColor = Color.White;
                    en_password.BackgroundColor = Color.White;
                    en_password.TextColor = Color.Black;
                    CheckRecField();
                }
            }
            
        }

        public void OnRepPasswordTextChanged(object sender, EventArgs e)
        {
            lb_rep_password_er.IsVisible = false;
            fr_rep_password.BorderColor = Color.White;
            fr_rep_password.BackgroundColor = Color.White;
            en_rep_password.BackgroundColor = Color.White;
            en_rep_password.TextColor = Color.Black;

            if (lb_conc_pass_er.IsVisible == true && fr_password.BorderColor == Color.White && en_password.Text != "" && en_password.Text != null)
            {
                if (en_password.Text == en_rep_password.Text)
                {
                    lb_conc_pass_er.IsVisible = false;
                }
            }
        }

        public void OnRepPasswordFocused(object sender, EventArgs e)
        {
            fr_rep_password.BorderColor = Color.SpringGreen;
            lb_conc_pass_er.IsVisible = false;
        }

        public void OnRepPasswordUnfocused(object sender, EventArgs e)
        {
            fr_rep_password.BorderColor = Color.White;
            if (en_rep_password.Text == "" || en_rep_password.Text == null) return;
            else
            {
                Match match = re_password.Match(en_rep_password.Text);
                if (!match.Success)
                {
                    lb_rep_password_er.IsVisible = true;
                    fr_rep_password.BorderColor = Color.FromRgb(194, 85, 85);
                    fr_rep_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_rep_password.BackgroundColor = Color.FromRgb(255, 187, 187);
                    en_rep_password.TextColor = Color.FromRgb(194, 85, 85);
                }
                else if (en_rep_password.Text != en_password.Text && en_password.Text != null && en_password.Text != "")
                {
                    lb_conc_pass_er.IsVisible = true;
                }
                else
                {
                    lb_rep_password_er.IsVisible = false;
                    fr_rep_password.BorderColor = Color.White;
                    fr_rep_password.BackgroundColor = Color.White;
                    en_rep_password.BackgroundColor = Color.White;
                    en_rep_password.TextColor = Color.Black;
                    CheckRecField();
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
        #endregion
    }
}