using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        public User us = DbWorking.User();
        public EditProfile()
        {
            InitializeComponent();
            Entry1.Text = us.firstName;
            Entry2.Text = us.lastName;
            Entry3.Text = us.DOB.ToString("d");
            if(us.gender == "М")
            {
                RadioButton1.IsChecked = true;
            }
            else
            {
                RadioButton2.IsChecked = true;
            }
        }

        async private void Button1_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            if (RadioButton1.IsChecked == true)
            {
                us.gender = "М";
            }
            else
            {
                us.gender = "Ж";
            }
            DbWorking.EditProfile(us.phone, Entry1.Text, Entry2.Text, us.gender);
            await Navigation.PopAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            DbWorking.UpdateToIO(0, string.Empty);
            Application.Current.MainPage = new SplashPage();
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

    }
}