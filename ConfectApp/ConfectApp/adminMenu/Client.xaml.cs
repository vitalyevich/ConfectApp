using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.adminMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Client : ContentPage
    {
        public Client()
        {
            InitializeComponent();
            User profile = DbWorking.ViewClients();
            label1.Text = profile.firstName;
            label2.Text = profile.Id;
            label3.Text = profile.lastName;
            label4.Text = profile.phone;
            label5.Text = profile.gender;
            label6.Text = profile.dob;
        }
        private void Refresh_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            Application.Current.MainPage = new AdminMenu(2);
        }
    }
}