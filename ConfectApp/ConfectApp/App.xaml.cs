using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            MainPage = new SplashPage();
        }

        protected override void OnSleep()
        {
           // DbWorking.Free(); 
        }

        protected override void OnResume() 
        {
            
        }
    }
}
