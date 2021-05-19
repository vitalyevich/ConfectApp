using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.adminMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Courier : ContentPage
    {
        public Courier()
        {
            InitializeComponent();
            User u = DbWorking.ViewCourier();
            label1.Text = u.firstName;
            label2.Text = u.phone;
            label3.Text = u.zone;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCourier());
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            Application.Current.MainPage = new AdminMenu(8);
        }

    }
}