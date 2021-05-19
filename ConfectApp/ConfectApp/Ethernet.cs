using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfectApp
{
    static class Ethernet
    {
        public static void CheckConnection()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.MainPage = new Errors();
            }
        }
        public static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckConnection();
        }
    }
}
