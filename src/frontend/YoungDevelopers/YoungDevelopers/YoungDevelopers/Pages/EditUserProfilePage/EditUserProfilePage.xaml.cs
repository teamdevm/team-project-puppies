using DogsCompanion.Api.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoungDevelopers.Utils;

namespace YoungDevelopers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserProfilePage : ContentPage
    {
        #region Инициализация 

        private UserInfo user;
        private ScrollView scrollview;
        private StackLayout layout;
        private Label lb_lastname, lb_firstname, lb_patronymic, lb_birthdate, lb_lastname_er, lb_firstname_er, lb_patronymic_er, lb_conc_pass_er, lb_main_fields, lb_update_er;
        private ControlEntry en_lastname, en_firstname, en_patronymic;
        private Frame fr_lastname, fr_firstname, fr_patronymic, fr_birthdate;
        private Button bt_save, bt_editemail, bt_editphone, bt_editpass;
        private DatePickerControl dp_birthdate;
        DogsCompanionClient dogsCompanionClient = DataControl.dogsCompanionClient;
        private Regex
            re_lastname = new Regex(RegexConstants.LastName);

        #endregion

        public EditUserProfilePage()
        {
            Title = "Редактирование профиля";
            layout = new StackLayout();
            scrollview = new ScrollView();
            layout.Orientation = StackOrientation.Vertical;
            layout.BackgroundColor = Color.FromRgb(242, 242, 242);

            // Получение данных
            user = DataControl.GetCurrentUserItem();

            #region Элементы страницы

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
                Placeholder = user.LastName.ToString(),
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
                Text = "Имя",
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
                Placeholder = user.FirstName,
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
                Placeholder = user.MiddleName.ToString(),
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
                Date = user.BirthDate == null ? DateTime.Now : DateTime.Parse(user.BirthDate.ToString()),
                FontFamily = "Cascadia Code Light",
                Margin = new Thickness(-5, -15, 0, -12),
                TextColor = Color.Gray,
                Format = "dd.MM.yyyy",
            };

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
                BackgroundColor = Color.FromRgb(105,233,165),
                CornerRadius = 10,
                WidthRequest = 370,
                HeightRequest = 40,
                Margin = new Thickness(0, 0, 0, 10),
            };

            layout.Children.Add(bt_save);
            bt_save.Clicked += OnSaveClicked;

            lb_update_er = new Label()
            {
                IsVisible = false,
                FontFamily = "Cascadia Code Light",
                Text = "Ошибка при обновлении информации",
                Margin = new Thickness(15, -5, 0, -1),
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Red,
            };

            layout.Children.Add(lb_update_er);

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

            layout.Children.Add(lb_main_fields);

            bt_editemail = new Button()
            {
                Text = "Редактировать почту",
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Cascadia Code Light",
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(105,233,165),
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
                BackgroundColor = Color.FromRgb(105,233,165),
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
                BackgroundColor = Color.FromRgb(105,233,165),
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
            UpdateFieldsFromServer();
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
            fr_lastname.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnLastNameUnfocused(object sender, EventArgs e)
        {
            fr_lastname.BorderColor = Color.White;
            if (en_lastname.Text == "" || en_lastname.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_lastname.Text);
                if (!match.Success || en_lastname.Text.Length > 29)
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
            lb_main_fields.IsVisible = false;
        }

        public void OnFirstNameFocused(object sender, EventArgs e)
        {
            fr_firstname.BorderColor = Color.FromRgb(105,233,165);
        }
        public void OnFirstNameUnfocused(object sender, EventArgs e)
        {
            fr_firstname.BorderColor = Color.White;
            if (en_firstname.Text == "" || en_firstname.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_firstname.Text);
                if (!match.Success || en_firstname.Text.Length > 29)
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
            fr_patronymic.BorderColor = Color.FromRgb(105,233,165);
        }

        public void OnPatronymicUnfocused(object sender, EventArgs e)
        {
            fr_patronymic.BorderColor = Color.White;
            if (en_patronymic.Text == "" || en_patronymic.Text == null) return;
            else
            {
                Match match = re_lastname.Match(en_patronymic.Text);
                if (!match.Success || en_patronymic.Text.Length > 29)
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
        
        public async void OnSaveClicked(object sender, EventArgs e)
        {
            lb_update_er.IsVisible = false;

            string convertedBirthDate = null;
            if (user.BirthDate != null)
            {
                convertedBirthDate = DateTime.Parse(user.BirthDate.ToString()).ToString("dd.MM.yyyy");
            }

            if (
                (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy") 
                    || dp_birthdate.Date.ToString("dd.MM.yyyy") == convertedBirthDate) 
                
                && en_firstname.Text == "" && en_lastname.Text == "" && en_patronymic.Text == "")
            {
                return;
            }
            else
            {
                if (fr_lastname.BorderColor == Color.FromRgb(194, 85, 85) || fr_firstname.BorderColor == Color.FromRgb(194, 85, 85) || fr_patronymic.BorderColor == Color.FromRgb(194, 85, 85) || fr_birthdate.BorderColor == Color.FromRgb(194, 85, 85))
                {
                    return;
                }
                else
                {
                    UpdateUser updateUser = new UpdateUser();

                    if (en_firstname.Text == "")
                    {
                        updateUser.FirstName = user.FirstName;
                    }
                    else
                    {
                        updateUser.FirstName = en_firstname.Text;
                    }

                    if (en_lastname.Text == "")
                    {
                        updateUser.LastName = user.LastName;
                    }
                    else
                    {
                        updateUser.LastName = en_lastname.Text;
                    }
                    if (en_patronymic.Text == "")
                    {
                        updateUser.MiddleName = user.MiddleName;
                    }
                    else
                    {
                        updateUser.MiddleName = en_patronymic.Text;
                    }

                    if (dp_birthdate.Date.ToString("dd.MM.yyyy") == DateTime.Now.Date.ToString("dd.MM.yyyy"))
                    {
                        updateUser.BirthDate = user.BirthDate;
                    }
                    else
                    {
                        updateUser.BirthDate = DateTime.SpecifyKind(dp_birthdate.Date, DateTimeKind.Utc);
                    }

                    try
                    {
                        await dogsCompanionClient.UpdateUserAsync(updateUser);

                        var userInfo = DataControl.GetCurrentUserItem();
                        userInfo.FirstName = updateUser.FirstName;
                        userInfo.LastName = updateUser.LastName;
                        userInfo.MiddleName = updateUser.MiddleName;
                        userInfo.BirthDate = updateUser.BirthDate;
                        DataControl.SetUserInfoItem(userInfo);

                        await Navigation.PopAsync();
                    }
                    catch (ApiException apiExc)
                    {
                        if (apiExc.StatusCode == StatusCodes.Status503ServiceUnavailable)
                        {
                            lb_update_er.Text = "Сервис недоступен";
                            lb_update_er.IsVisible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("", "Непредвиденная ошибка", "OK");
                    }
                }
            } 
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

        public async void UpdateFieldsFromServer()
        {
            try
            {
                UserInfo updateUser = (UserInfo)await dogsCompanionClient.GetUserInfoAsync();

                en_lastname.Placeholder = updateUser.LastName.ToString();
                en_firstname.Placeholder = updateUser.FirstName;
                en_patronymic.Placeholder = updateUser.MiddleName.ToString();
                dp_birthdate.Date = updateUser.BirthDate == null ? DateTime.Now : DateTime.Parse(user.BirthDate.ToString());
            }
            catch (ApiException apiExc)
            {
                await DisplayAlert("", "Сервис недоступен", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("", "Непредвиденная ошибка", "OK");
            }
        }
    }
}

