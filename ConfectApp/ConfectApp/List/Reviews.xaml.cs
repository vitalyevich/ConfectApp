using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reviews : ContentPage
    {
        public Reviews()
        {
            InitializeComponent();
            formList.Clear();
            int check = DbWorking.CheckIO();
            if (check == 1)
            {
                int isMenu = DbWorking.isCheckMenu();
                if (isMenu == 0)
                {
                    DbWorking.ViewReviews(1);
                }
                else
                {
                    DbWorking.ViewReviews(0);
                }
            }
            else
            {
                DbWorking.ViewReviews(1);
            }

            StackLayout stack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#F8F8F8"),
                Spacing = 0,
                Children =
                {
                new Line { X1=500, Stroke=Brush.GhostWhite, HorizontalOptions=LayoutOptions.CenterAndExpand, Margin= new Thickness(0,0,0,5)},
                }
            };

            foreach (Frame frame in formList)
            {
                stack.Children.Add(frame);
            }
            ScrollView scrollView = new ScrollView { Content = stack };
            Content = scrollView;
        }
        public static List<Frame> formList = new List<Frame>();

        public static void CreateFormAdmin(Review rev)
        {
            string rating = String.Empty;
            int check;
            for (check = 0; check < rev.sum; check++)
            {
                rating = rating + "★";
            }
            if (check < 5)
            {
                int check1 = 5 - check;
                for (check = 0; check < check1; check++)
                    rating = rating + "☆";
            }
            Image image = new Image
            {
                Source = "close.png",
                Aspect = Aspect.AspectFit,
                HeightRequest = 25,
                WidthRequest = 25,
                Margin = new Thickness(0, -5, 0, 0)
            };
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 210,
                Children =
                {
                    new Label
                  {
                Text = rev.date.ToShortDateString() + " " + rev.date.ToShortTimeString(),
                TextColor = Color.FromHex("#A0A0A0"),
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 13
                  },
                     image
                }
            };

            Frame frame = new Frame
            {
                Padding = new Thickness(0, 0, 0, 10),
                Margin = new Thickness(20, 10, 20, 0),
                CornerRadius = 8,
                HasShadow = false,
                Content =
              new StackLayout
              {
                  Margin = new Thickness(10),
                  Children =
                  {
                  stackLayout,
                   new Label
                  {
                Text = rating,
                Margin = new Thickness(0,-4,0,0),
                TextColor = Color.Black,
                FontFamily="Ubuntu-Light.ttf#Ubuntu",
                FontSize = 14
                  },
                    new Label
                  {
                Text = rev.text,
                Margin = new Thickness(0,2,0,0),
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 17
                  }
                  }
              }
            };

            int id = rev.id;
            var TapGestureRecognizer = new TapGestureRecognizer();
            TapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
            {
                Vibration.Vibrate(TimeSpan.FromSeconds(0.05));
                formList.Remove(frame);
                DbWorking.DeleteReviewIndex(id);
                Application.Current.MainPage = new AdminMenu(4);
            };
            image.GestureRecognizers.Add(TapGestureRecognizer);

            formList.Add(frame);
        }
        public static void CreateFormUser(Review rev)
        {
            string rating = String.Empty;
            int check;
            for (check = 0; check < rev.sum; check++)
            {
                rating = rating + "★";
            }
            if (check < 5)
            {
                int check1 = 5 - check;
                for (check = 0; check < check1; check++)
                    rating = rating + "☆";
            }

            Frame frame = new Frame
            {
                Padding = new Thickness(0, 0, 0, 10),
                Margin = new Thickness(20, 10, 20, 0),
                CornerRadius = 8,
                HasShadow = false,
                Content =
                  new StackLayout
                  {
                      Margin = new Thickness(10),
                      Children =
                      {
                  new Label
                  {
                Text = rev.date.ToShortDateString() + " " + rev.date.ToShortTimeString(),
                TextColor = Color.FromHex("#A0A0A0"),
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 13
                  },
                   new Label
                  {
                Text = rating,
                Margin = new Thickness(0,-4,0,0),
                TextColor = Color.Black,
                FontFamily="Ubuntu-Light.ttf#Ubuntu",
                FontSize = 14
                  },
                    new Label
                  {
                Text = rev.text,
                Margin = new Thickness(0,2,0,0),
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 17
                  }
                      }
                  }
            };
            formList.Add(frame);
        }
        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            await Navigation.PushAsync(new AddReview());
        }
    }
}