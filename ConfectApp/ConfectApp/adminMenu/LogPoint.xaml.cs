using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.adminMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPoint : ContentPage
    {
        public LogPoint()
        {
            InitializeComponent();
            User log = DbWorking.LogPoint();
            label1.Text = log.Date;
            label2.Text = log.phone;
            label3.Text = log.tranc;
            label4.Text = log.Point;
        }

        private void Refresh_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            Application.Current.MainPage = new AdminMenu(6);
        }
    }
}