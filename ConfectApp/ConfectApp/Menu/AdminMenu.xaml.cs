using ConfectApp.adminMenu;
using ConfectApp.List;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminMenu : MasterDetailPage
    {
        public AdminMenu(int check)
        {
            InitializeComponent();
            IsPresented = false;
            ListMenu(check);
        }

        public void ListMenu(int check)
        {
            if (check == 0)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Stats)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
            else if (check == 2)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Client)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
            else if (check == 8)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Courier)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
            else if (check == 1)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ConfectApp.List.Profile)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black,
                };
            }
            else if (check == 6)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LogPoint)))
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
            else if (check == 4)
            {
                Detail = new NavigationPage(new Reviews())
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
            else if (check == 3)
            {
                Detail = new NavigationPage(new Orders())
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ConfectApp.List.Profile)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black,
            };
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Client)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage(new Orders())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage(new Reviews())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };

        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Stats)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };

        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LogPoint)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        private void Button_Clicked_7(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Push)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }

        private void Button_Clicked_8(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            Device.OpenUri(new Uri("https://www.instagram.com/vittalyevich/"));
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Courier)))
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
        }
    }
}