using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Company : ContentPage
    {
        public Company()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Ethernet.Connectivity_ConnectivityChanged;
            CheckDate();
            CheckTime();
        }

        public bool check = false;
        public void CheckTime()
        {
            DateTime date = DateTime.Now;
            TimeSpan timeOfDay = date.TimeOfDay;
            TimeSpan start = new TimeSpan(10, 30, 0);
            TimeSpan stop = new TimeSpan(22, 30, 0);
            if (timeOfDay > stop || timeOfDay < start)
            {
                DisplayActionSheet("Кондитер", "Ок", null, "Доставка и предзаказ осуществляются 10:30 - 22:30", null, null);
            }

        }
        public void CheckDate()
        {
            DateTime date = DateTime.Now;
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                Label7.TextColor = Color.Black;
                Label7.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                Label6.TextColor = Color.Black;
                Label6.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else if (date.DayOfWeek == DayOfWeek.Friday)
            {
                Label5.TextColor = Color.Black;
                Label5.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else if (date.DayOfWeek == DayOfWeek.Thursday)
            {
                Label4.TextColor = Color.Black;
                Label4.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
            {
                Label3.TextColor = Color.Black;
                Label3.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else if (date.DayOfWeek == DayOfWeek.Tuesday)
            {
                Label2.TextColor = Color.Black;
                Label2.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
            else
            {
                Label1.TextColor = Color.Black;
                Label1.FontFamily = "Ubuntu-Regular.ttf#Ubuntu";
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ethernet.CheckConnection();
        }
    }
}