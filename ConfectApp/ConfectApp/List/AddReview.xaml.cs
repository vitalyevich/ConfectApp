using ConfectApp.Menu;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReview : ContentPage
    {
        public AddReview()
        {
            InitializeComponent();
            Editor1.Text = String.Empty;
        }

        public bool button
        {
            get
            {
                Button1.IsEnabled = (Rating1.Rating != 0 && Rating2.Rating != 0 && Rating3.Rating != 0 && Editor1.Text != String.Empty) ? true : false;
                return Button1.IsEnabled;
            }
        }
        public bool checkRating(int check)
        {
            if (check <= 0 || check > 5)
            {
                return true;
            }
            return false;
        }
        private int rating;
        public Int32 Rating
        {
            get
            {
                if (rating <= 0)
                {
                    rating = 1;
                }
                else if (rating > 5)
                {
                    rating = 5;
                }
                return rating;
            }
            set { rating = value; }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));

            Review review = new Review
            {
                speed = Rating1.Rating,
                kitchen = Rating2.Rating,
                service = Rating3.Rating,
                text = Editor1.Text
            };
            DbWorking.AddReview(review);
            CheckUserMenu();
        }

        public void CheckUserMenu()
        {
            int check = DbWorking.CheckIO();
            if (check == 1)
            {
                int isMenu = DbWorking.isCheckMenu();
                if (isMenu == 0)
                {
                    Application.Current.MainPage = new UserMenu(4);
                }
                else
                {
                    Application.Current.MainPage = new AdminMenu(4);
                }
            }
            else
            {
                Application.Current.MainPage = new GuestMenu(4);
            }
        }

        private void Editor1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = button;
        }

        private void Rating3_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Button1.IsEnabled = button;
            if (checkRating(Rating3.Rating)) Rating3.Rating = Rating;
        }

        private void Rating2_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Button1.IsEnabled = button;
            if (checkRating(Rating2.Rating)) Rating2.Rating = Rating;
        }

        private void Rating1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Button1.IsEnabled = button;
            if (checkRating(Rating1.Rating)) Rating1.Rating = Rating;
        }
    }
}