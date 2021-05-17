using Android.Content;
using ConfectApp.adminMenu;
using ConfectApp.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class GuestMenu : MasterDetailPage   
    {
        public GuestMenu(int check)
        {
            InitializeComponent();
            IsPresented = false;
            ListMenu(check);
        }
        public void ListMenu(int check)
        {
            if (check == 0)
            {
                Detail = new Navigate(1);
            }
            else if (check == 4)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Reviews)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
        }
        // person
        private void Button_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new Navigate(2);
        }
        // backet
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new Navigate(3);
        }
        // company
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Company)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }
        // menu
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new Navigate(1);
        }
        // reviews
        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Reviews)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }
        // promotions
        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new Navigate(4);
        }
        // history
        private void Button_Clicked_6(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(History)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }
        // news
        private void Button_Clicked_7(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(News)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }
        // instagram
        private void Button_Clicked_8(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            Device.OpenUri(new Uri("https://www.instagram.com/vittalyevich/"));
        }
        // input
        private void Button_Clicked_9(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;

            Detail = new NavigationPage(new MainPage())
           {
               BarBackgroundColor = Color.White,
               BarTextColor = Color.Black,
           };
        }
    }
}