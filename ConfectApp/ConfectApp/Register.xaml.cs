using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Ethernet.Connectivity_ConnectivityChanged;
            Entry2.Text = String.Empty;
            Entry3.Text = String.Empty;
            Entry4.Text = String.Empty;
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
        }

        private void RadioButton2_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

        private void RadioButton1_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

        public bool buttonCheck
        {
            get
            {
                Button1.IsEnabled = (Entry1.Text != String.Empty && Entry2.Text != String.Empty &&
                    Entry3.Text != String.Empty && Entry4.Text != String.Empty &&
                    (RadioButton1.IsChecked == true || RadioButton2.IsChecked == true)) ? true : false;
                return Button1.IsEnabled;
            }
        }

        private void Entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }
        private void Entry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }
        private void Entry3_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }
        private void Entry4_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ethernet.CheckConnection();
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(3));
            User user = new User()
            {
                firstName = Entry3.Text,
                lastName = Entry4.Text,
                password = Entry2.Text,
                phone = Entry1.Text,
                DOB = date.Date
            };

            user.gender = (RadioButton1.IsChecked == true) ? "М" : "Ж";

            if (DbWorking.RegUser(user))
            {
                DisplayActionSheet("Кондитер", "Ок", null, "Вы успешно прошли регистрацию!", null, null);
            }
            else
            {
                DisplayActionSheet("Кондитер", "Ок", null, "Такой пользователь уже зарегистрирован!", null, null);
            }
            Navigation.PopAsync();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            date.Date = e.NewDate;
        }

        private void Entry1_Focused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.LightSkyBlue;
        }

        private void Entry1_Unfocused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry2_Focused(object sender, FocusEventArgs e)
        {
            Frame2.BorderColor = Color.LightSkyBlue;
        }

        private void Entry2_Unfocused(object sender, FocusEventArgs e)
        {
            Frame2.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry3_Focused(object sender, FocusEventArgs e)
        {
            Frame3.BorderColor = Color.LightSkyBlue;
        }

        private void Entry3_Unfocused(object sender, FocusEventArgs e)
        {
            Frame3.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry4_Focused(object sender, FocusEventArgs e)
        {
            Frame4.BorderColor = Color.LightSkyBlue;
        }

        private void Entry4_Unfocused(object sender, FocusEventArgs e)
        {
            Frame4.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void date_Focused(object sender, FocusEventArgs e)
        {
            Frame5.BorderColor = Color.LightSkyBlue;
        }

        private void date_Unfocused(object sender, FocusEventArgs e)
        {
            Frame5.BorderColor = Color.FromHex("#E8E8E8");
        }
    }
}