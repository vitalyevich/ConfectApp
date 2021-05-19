using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfectApp
{
    public partial class Errors : ContentPage
    {
        public Errors()
        {
            InitializeComponent();
            DisplayActionSheet("Кондитер", "Ок", null, "Ошибка соединения! TypeError:\nNetwork request failed", null, null);
        }
        private void Button1_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                SplashPage.CheckIOAndroid();
            }
            else
            {
                DisplayActionSheet("Кондитер", "Ок", null, "Ошибка соединения! TypeError:\nNetwork request failed", null, null);
            }
        }
    }
}