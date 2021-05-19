using ConfectApp.Menu;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class News : ContentPage
    {
        public static List<Frame> formList = new List<Frame>();

        public RefreshView refreshView = new RefreshView
        {
            RefreshColor = Color.Black,
            BackgroundColor = Color.White
        };
        public static void CreateForm(Msg msg)
        {
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
                Text = msg.date.ToShortDateString() + " " + msg.date.ToShortTimeString(),
                TextColor = Color.FromHex("#A0A0A0"),
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 13
                  },
                   new Label
                  {
                Text = msg.header,
                Margin = new Thickness(0,2,0,0),
                TextColor = Color.Black,
                FontFamily="Ubuntu-Bold.ttf#Ubuntu",
                FontSize = 18
                  },
                    new Label
                  {
                Text = msg.txt,
                Margin = new Thickness(0,-2,0,0),
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 15
                  }
                      }
                  }
            };
            formList.Add(frame);
        }
        public void addForm()
        {
            formList.Clear();
            DbWorking.ViewMsg();
            StackLayout stack = new StackLayout
            {
                BackgroundColor = Color.FromHex("#F8F8F8"),
                Spacing = 0,
                Children =
                {
                new Line { X1=500, Stroke=Brush.GhostWhite, HorizontalOptions=LayoutOptions.CenterAndExpand, Margin= new Thickness(0,0,0,5)},
                }
            };

            foreach (Frame frame in News.formList)
            {
                stack.Children.Add(frame);
            }
            ScrollView scrollView = new ScrollView { Content = stack };

            refreshView.Content = scrollView;
            refreshView.Refreshing += RefreshView_Refreshing;

            Content = refreshView;
        }
        public News()
        {
            InitializeComponent();
            addForm();
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            refreshView.IsRefreshing = false;
            Application.Current.MainPage = new UserMenu(8);
        }
    }
}