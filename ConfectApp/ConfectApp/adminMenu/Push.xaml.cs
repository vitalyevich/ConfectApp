using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.adminMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Push : ContentPage
    {
        public Push()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Ethernet.Connectivity_ConnectivityChanged;
            Entry1.Text = String.Empty;
            Editor1.Text = String.Empty;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ethernet.CheckConnection();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            string click = await DisplayActionSheet("Пуш-рассылка", "Нет", "Отправить", "Не спеши отправлять, убедись, что всё правильно написано.");
            if (click == "Отправить")
            {
                DbWorking.AddPush(Entry1.Text, Editor1.Text);
                await DisplayActionSheet("Пуш-рассылка", "Ок", null, "Отправка прошла успешно!");
                Entry1.Text = String.Empty;
                Editor1.Text = String.Empty;
            }
        }

        public bool button
        {
            get
            {
                Button1.IsEnabled = (Entry1.Text != String.Empty && Editor1.Text != String.Empty) ? true : false;
                return Button1.IsEnabled;
            }
        }

        private void Entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = button;
        }

        private void Editor1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = button;
        }
    }
}