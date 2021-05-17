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
    public partial class AddCourier : ContentPage
    {
        public AddCourier()
        {
            InitializeComponent();
            Entry1.Text = String.Empty;
        }

        async private void Button1_Clicked(object sender, EventArgs e)
        {
            User user = new User
            {
                firstName = Entry1.Text,
                phone = Entry2.Text,
            };
            if (!Check.IsChecked) user.zone = picker.Items[picker.SelectedIndex];

            if (Check.IsChecked)
            {
                DbWorking.DeleteCourier(user.phone);
                await DisplayActionSheet("Курьеры", "Ок", null, "Вы успешно удалили курьера!");
            }
            else
            {
                if (DbWorking.AddCourier(user))
                {
                    await DisplayActionSheet("Курьеры", "Ок", null, "Вы успешно добавили нового курьера!");
                }
                else
                {
                    await DisplayActionSheet("Курьеры", "Ок", null, "Вы успешно обновили данные курьера!");
                }
            }
            Application.Current.MainPage = new AdminMenu(8);
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Check.IsChecked) 
            {
                Entry1.IsEnabled = false;
                picker.IsEnabled = false;
                Check.Color = Color.FromHex("ff0000");
                Frame2.BorderColor = Color.FromHex("ff0000");
                picker.Title = String.Empty;
                Button1.IsEnabled = true;
            }
            else
            {
                Entry1.IsEnabled = true;
                picker.IsEnabled = true;
                Check.Color = Color.FromHex("#E8E8E8");
                Frame2.BorderColor = Color.FromHex("#E8E8E8");
                picker.Title = "Указать новую";
                Button1.IsEnabled = buttonCheck;
            }
        }

        private void Entry1_Focused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.LightSkyBlue;
        }
        private void Entry1_Unfocused(object sender, FocusEventArgs e)
        {
            Frame1.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry2_Focused(object sender, FocusEventArgs e)
        {
            if (!Check.IsChecked) Frame2.BorderColor = Color.LightSkyBlue;

        }

        private void Entry2_Unfocused(object sender, FocusEventArgs e)
        {
            if (!Check.IsChecked) Frame2.BorderColor = Color.FromHex("#E8E8E8");
        }

        private void Entry3_Focused(object sender, FocusEventArgs e)
        {
            Frame3.BorderColor = Color.LightSkyBlue;
        }
        private void Entry3_Unfocused(object sender, FocusEventArgs e)
        {
            Frame3.BorderColor = Color.FromHex("#E8E8E8");
        }

        private string text; 
        public string checkLogin
        {
            get
            {
                if (Entry2.Text.Length == 13)
                {
                    if (Int64.TryParse(Entry2.Text.TrimStart('+', '3'), out long number) && Entry2.Text.Contains("+375"))
                    {
                        text = String.Format("{0:+3## (##) ###-##-##}", number);
                        return text;
                    }
                }
                return Entry2.Text;
            }
        }

        public bool buttonCheck
        {
            get
            {
                Button1.IsEnabled = (Entry1.Text != String.Empty && Entry2.Text != String.Empty && picker.SelectedIndex != -1 || Check.IsChecked) ? true : false;
                return Button1.IsEnabled;
            }
        }

        private void Entry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry2.Text = checkLogin;
            Button1.IsEnabled = buttonCheck;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

        private void Entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button1.IsEnabled = buttonCheck;
        }

    }
}