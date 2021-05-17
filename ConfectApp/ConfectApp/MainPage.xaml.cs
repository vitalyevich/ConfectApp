using ConfectApp.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfectApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Ethernet.Connectivity_ConnectivityChanged;
            Entry2.Text = String.Empty;
        }

        public bool buttonCheck
        {
            get
            {
                Button1.IsEnabled = (Entry1.Text != String.Empty && Entry2.Text != String.Empty) ? true : false;
                return Button1.IsEnabled;
            }
        }

        private void Entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry1.Text = checkLogin;
            Button1.IsEnabled = buttonCheck;        
        }
        private void Entry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ethernet.CheckConnection();
        }

        void Button1_Clicked(object sender, EventArgs args)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(3));

            if (DbWorking.CheckUser(Entry1.Text, Entry2.Text))
            { 
                DbWorking.UpdateToIO(1, Entry1.Text);   
                if (DbWorking.isCheckMenu() == 0)
                {
                    Application.Current.MainPage = new UserMenu(0);
                }
                else
                {
                    Application.Current.MainPage = new AdminMenu(0);
                }
            }
            else
            {
                DisplayActionSheet("Кондитер", "Ок", null, "Вы ввели неверный логин или пароль! Повторите попытку", null, null);
            }

        }

        async private void Button2_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));

            await Navigation.PushAsync(new Register());
        }

        private string text;
        public string checkLogin
        {
            get
            {
                if (Entry1.Text.Length == 13)
                {
                    if (Int64.TryParse(Entry1.Text.TrimStart('+', '3'), out long number) && Entry1.Text.Contains("+375"))
                    {
                        text = String.Format("{0:+3## (##) ###-##-##}", number);
                        return text;
                    }
                }
                return Entry1.Text;
            }
        }

        private void Entry1_Focused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.LightSkyBlue;
        }

        private void Entry2_Focused(object sender, FocusEventArgs e)
        {
            Frame2.BorderColor = Color.LightSkyBlue;
        }

        private void Entry2_Unfocused(object sender, FocusEventArgs e)
        {
            Frame2.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry1_Unfocused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.FromHex("#E8E8E8");
        }
    }
}