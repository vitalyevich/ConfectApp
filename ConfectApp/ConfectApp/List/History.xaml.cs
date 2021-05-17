using ConfectApp.Menu;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public History()
        {
            int check = DbWorking.CheckIO();
            if (check == 0)
            {
                Application.Current.MainPage = new GuestMenu(0);
            }
            else
            {
                listFrame.Clear();
                InitializeComponent();
                DbWorking.ViewHistory();
                foreach (Frame frame in listFrame)
                {
                    stackout.Children.Add(frame);
                }
                Scroll.Content = stackout;
                Refresh.Content = Scroll;
            }
        }

        public static List<Frame> listFrame = new List<Frame>();
        public static void CreateForms(Order history)
        {
            Frame frame = new Frame
            {
                Padding = new Thickness(0,0,0,5),
                Margin = new Thickness(20,0,20,10), 
                CornerRadius = 8,
                HasShadow = false
            };
            StackLayout stack = new StackLayout
            {
                Margin = new Thickness(20)
            };

            frame.Content = stack; 

            Button button = new Button
            {
                Margin = new Thickness(130, -2, 0, 0),
                Text = history.status, 
                TextTransform = TextTransform.Lowercase,
                TextColor = Color.White,
                BackgroundColor = Color.LightGreen, 
                FontSize = 14,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                Padding = new Thickness(0),
                HeightRequest = 20,
                WidthRequest = 60
            };
            ////////////
            if (history.status == "Новый")
            {
                button.Text = "в ожидании";
                button.WidthRequest = 90;
                button.Margin = new Thickness(100, -2, 0, 0);
                button.BackgroundColor = Color.Orange;
            }
            else if(history.status == "Отмена")
            {
                button.BackgroundColor = Color.Red;
            }
            else if(history.status == "Доставлен")
            {
                button.WidthRequest = 90;
                button.Margin = new Thickness(100, -2, 0, 0);
            }
            //////////
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                new Label
                {
                    Text = history.date.ToShortDateString() +" "+ history.date.ToShortTimeString(),
                    Margin = new Thickness(0,0,20,0),
                    TextColor = Color.FromHex("#A0A0A0"),
                    FontSize = 12,
                    FontFamily="Ubuntu-Light.ttf#Ubuntu"
                },
                button
                }
            };
            stack.Children.Add(stackLayout);

            if(history.nomination == "Самовывоз")
            {
                Label label = new Label
                {
                    Text = "Самовывоз:",
                    TextColor = Color.Black, 
                    FontSize = 16, 
                    FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
                };
                stack.Children.Add(label);
            }
            else
            {
                Label label = new Label
                {
                    Text = "Доставка по адресу:",
                    TextColor = Color.Black,
                    FontSize = 16,
                    FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
                };
                stack.Children.Add(label);
            }

            stack.Children.Add(new Label
            {
                Margin = new Thickness(0,-5,0,0) ,
                Text = history.GPS, 
                TextColor = Color.Black,
                FontSize = 16 ,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });

            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = "- - -",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = "Комментарий:", 
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });

            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0), 
                Text = history.comment,
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = "- - -",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text=$"Заберу заказ через {history.time}", 
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = "- -",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label 
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = history.products,
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label
            {
                Margin = new Thickness(0, -5, 0, 0),
                Text = "- -",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });

            stack.Children.Add(new Label 
            {
                Text = $"Форма оплаты: {history.payment}",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label 
            {
                Text = $"Стоимость блюд: {history.total} р.",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });
            stack.Children.Add(new Label 
            {
                Text = $"Итого: {history.total} р.",
                TextColor = Color.Black,
                FontSize = 16,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu"
            });

            listFrame.Add(frame);
         }

        private void Refresh_Refreshing(object sender, EventArgs e)
        {
            Refresh.IsRefreshing = false;
            Application.Current.MainPage = new UserMenu(6);
        }
    }
}