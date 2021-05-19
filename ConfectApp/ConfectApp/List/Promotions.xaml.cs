using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Promotions : ContentPage
    {
        public Promotions()
        {
            InitializeComponent();
        }
        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            await DisplayActionSheet("Кондитер", "Ок", null, "Промокод скопирован в буфер", null, null);
            await Clipboard.SetTextAsync("Confect");
        }

        async private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            await DisplayActionSheet("Кондитер", "Ок", null, "Промокод скопирован в буфер", null, null);
            await Clipboard.SetTextAsync("Start");

        }
    }
}