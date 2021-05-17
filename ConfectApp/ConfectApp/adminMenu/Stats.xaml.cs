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
    public partial class Stats : ContentPage
    {
        public Stats()
        {
            InitializeComponent();
            Review rev = DbWorking.StatsReviews();
            LabelSpeed.Text = rev.speed.ToString();
            LabelService.Text = rev.service.ToString();
            LabelKitchen.Text = rev.kitchen.ToString();
            LabelSum.Text = rev.sum.ToString() + " балл.";

            int quantity = DbWorking.StatsUsers();
            labelQuantity.Text = quantity.ToString();
            int orders = DbWorking.StatsOrders();
            labelOrders.Text = orders.ToString() + " заказ.";
            int profit = DbWorking.StatsProfit(); 
            labelProfit.Text = profit.ToString() + " р."; 

            labelDay.Text = DbWorking.StatsNumberOfOrders(0).ToString();
            labelWeek.Text = DbWorking.StatsNumberOfOrders(1).ToString();
            labelMonth.Text = DbWorking.StatsNumberOfOrders(2).ToString();
            labelYear.Text = DbWorking.StatsNumberOfOrders(3).ToString();
        }
    }
}