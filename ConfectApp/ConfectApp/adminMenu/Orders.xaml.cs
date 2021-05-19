using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ConfectApp.adminMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Orders : ContentPage
    {
        public Orders()
        {
            InitializeComponent();
            stacks.Clear();
            DbWorking.ViewOrders();

            foreach (StackLayout layout in stacks)
            {
                stack.Children.Add(layout);
                stack.Children.Add(new Line
                {
                    X1 = 580,
                    Stroke = Brush.LightGray,
                    HorizontalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, 5, 0, 5)
                });
            }
            Content = Refresh;
        }
        public static List<StackLayout> stacks = new List<StackLayout>();
        public static void CreateForm(Order usOrder)
        {
            Picker picker = new Picker
            {
                TitleColor = Color.LightGray,
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                FontSize = 14,
                Items =
                {
                "Новый", "Принят", "Доставлен", "Отмена"
                },
                Margin = new Thickness(20, -11, 0, 0)
            };

            /////////////////////////
            Button button = new Button
            {
                Text = "Нажми",
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 14,
                TextColor = Color.Black,
                BackgroundColor = Color.FromHex("#0000ffff"),
                Padding = new Thickness(0),
                TextTransform = TextTransform.None,
                Margin = new Thickness(-18, -26, 0, -15)

            };
            string txt = $"{usOrder.products}";
            button.Clicked += async (object sender, EventArgs e) =>
            {
                await App.Current.MainPage.DisplayAlert("Заказ", txt, "ОK");
            };
            //////////////////////

            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 5, 10, -5),
                Children =
                {
                    new Label
                    {
                        Text = usOrder.date.ToShortDateString() + " " + usOrder.date.ToShortTimeString(),
                        FontFamily ="Ubuntu-Light.ttf#Ubuntu", FontSize = 14, TextColor = Color.Black
                    },
                    new Label
                    {
                       Text = usOrder.firstName, FontFamily ="Ubuntu-Light.ttf#Ubuntu", FontSize = 14, TextColor = Color.Black,
                       Margin = new Thickness(10,0,15,0)

                    },
                    new Label
                    {
                       Text = usOrder.phone, FontFamily ="Ubuntu-Light.ttf#Ubuntu", FontSize = 14, TextColor = Color.Black,
                       Margin = new Thickness(0,0,10,0)
                     },
                    button,
                    new Label
                    {
                       Text = usOrder.total.ToString() + "р.", FontFamily ="Ubuntu-Light.ttf#Ubuntu", FontSize = 14, TextColor = Color.Black,
                       Margin = new Thickness(10,0,0,0)
                    },
                    picker
                }
            };
            picker.Title = usOrder.status;
            pickerColor(picker.Title, picker);
            string telephone = usOrder.phone;
            int id = usOrder.id;
            picker.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                string status = picker.Items[picker.SelectedIndex];
                DbWorking.UpdateOrder(status, telephone, id);
                pickerColor(status, picker);
            };

            picker.Focused += (object sender, FocusEventArgs e) =>
            {
                picker.TextColor = Color.LightSkyBlue;
                picker.TitleColor = Color.LightSkyBlue;
            };
            picker.Unfocused += (object sender, FocusEventArgs e) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    pickerColor(picker.Title, picker);
                }
                else
                {
                    pickerColor(picker.Items[picker.SelectedIndex], picker);
                }
            };
            stacks.Add(stackLayout);
        }

        public static void pickerColor(string txt, Picker picker)
        {
            if (txt == "Принят")
            {
                picker.TextColor = Color.LightGreen;
                picker.TitleColor = Color.LightGreen;
            }
            else if (txt == "Отмена")
            {
                picker.TextColor = Color.Red;
                picker.TitleColor = Color.Red;
            }
            else
            {
                picker.TitleColor = Color.Black;
                picker.TextColor = Color.Black;
            }
        }

        private void Refresh_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            Application.Current.MainPage = new AdminMenu(3);
        }
    }
}