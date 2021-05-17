using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.Emit;
using Xamarin.Forms;
using Xamarin.Essentials;
using ConfectApp;
using ConfectApp.Menu;

namespace ConfectApp
{
    public class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                WidthRequest = 440,
                HeightRequest = 440
            };
            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.3, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("77e4ff");
            this.Content = sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 1400, Easing.Linear);
            await splashImage.ScaleTo(1, 1300, Easing.Linear);

            CheckIOAndroid();
        }
        public static void CheckIOAndroid()
        {
            DbWorking.AddDeviceID();
            int check = DbWorking.CheckIO();
            if (check == 1)
            {
                int isMenu = DbWorking.isCheckMenu(); 
                if (isMenu == 0)
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
                Application.Current.MainPage = new GuestMenu(0);
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
