using ConfectApp.Menu;
using Plugin.DeviceInfo;
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
    public partial class Profile : ContentPage
    {
        public Profile() 
        {
            list.Clear();
            Connectivity.ConnectivityChanged += Ethernet.Connectivity_ConnectivityChanged;
            InitializeComponent();
            User us = DbWorking.ViewUser();
            Label1.Text = us.firstName;
            Label2.Text = us.phone;
            Label3.Text = us.DOB.ToString("d");
            Label5.Text = us.point.ToString() + " р.";
            Label4.Text = us.count.ToString() + " заказ";
            DbWorking.ViewPoint(us.phone);

            foreach (Frame frame in list)
            {
                stack.Children.Add(frame);
            }
            Scroll.Content = stack;
            stack2.Children.Add(Scroll);
            Refresh.Content = stack2;

        }
        public static List<Frame> list = new List<Frame>(); 
        public static void LogPoint(Order user) 
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                     new Label
                  {
                Text = user.date.ToString("MM MMM") + " " + user.date.ToShortTimeString(),
                TextColor = Color.FromHex("#A0A0A0"),
                Margin = new Thickness(10,0,0,0),
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 13
                  },
                      new Label
                  {
                Text ="+ " + user.point.ToString() + " руб.",
                Margin = new Thickness(220,0,0,0),
                TextColor = Color.LightGreen,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 14
                  }
                }
            };

            Frame frame = new Frame
            {
                HasShadow = false,
                Padding = new Thickness(0, 0, 0, 2),
                Margin = new Thickness(0, 5, 0, 5),
                Content =
                  new StackLayout
                  {
                      Margin = new Thickness(10),
                      Children =
                      {
                  stackLayout,
                   new Label
                  {
                Text = "Кэшбэк",
                Margin = new Thickness(10,-4,0,0),
                TextColor = Color.Black,
                FontFamily="Ubuntu-Bold.ttf#Ubuntu",
                FontSize = 16
                  }

                      }
                  }
            };
            list.Add(frame);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ethernet.CheckConnection();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            await Navigation.PushAsync(new EditProfile());
        }

        private void Refresh_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            int isMenu = DbWorking.isCheckMenu(); 
            if (isMenu == 0)
            {
                Application.Current.MainPage = new UserMenu(2);
            }
            else
            {
                Application.Current.MainPage = new AdminMenu(1);
            }
        }
    }
}