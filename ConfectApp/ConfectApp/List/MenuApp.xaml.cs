using ConfectApp.Menu;
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
    public partial class MenuApp : ContentPage
    {
        public MenuApp()
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
   
        public Color button
        {
            get
            {
                if(Button1.TextColor == Color.FromHex("#5adbfb") || 
                    Button2.TextColor == Color.FromHex("#5adbfb") || Button3.TextColor == Color.FromHex("#5adbfb"))
                {
                    Button1.TextColor = Color.Black;
                    Button2.TextColor = Color.Black;
                    Button3.TextColor = Color.Black;
                }
               return Color.FromHex("#5adbfb");
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            scrollView.ScrollToAsync(labelT, ScrollToPosition.Start, true);
            Button1.TextColor = button;

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            scrollView.ScrollToAsync(labelP, ScrollToPosition.Start, true);
            Button2.TextColor = button;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            scrollView.ScrollToAsync(labelD, ScrollToPosition.Start, true);
            Button3.TextColor = button;
        }
        private void Button_Snikers_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(1);
        }
        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(2);
        }
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(3);
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(4);
        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(5);
        }

        private void Button_Clicked_7(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(6);
        }

        private void Button_Clicked_8(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(8);
        }

        private void Button_Clicked_9(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(9);
        }

        private void Button_Clicked_10(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(10);
        }

        private void Button_Clicked_11(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(12);
        }

        private void Button_Clicked_12(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(13);
        }

        private void Button_Clicked_13(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(14);
        }

        private void Button_Clicked_14(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(15);
        }

        private void Button_Clicked_15(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
            DbWorking.AddToBasket(16);
        }
    }
}