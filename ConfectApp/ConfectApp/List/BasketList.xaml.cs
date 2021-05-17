using ConfectApp.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketList : ContentPage
    {
        public User us = DbWorking.ViewUser();
        public BasketList()
        {
            InitializeComponent();
            CreateForm(1);
            entry5.Text = us.lastName;
            entry2.Text = us.phone;
        }
   
        public static List<String> prod = new List<String>();
        public static void listPoducts(Product p)
        { 
            prod.Add($"{p.productName}, {p.productAmount}\n    {p.productPrice} р. x {p.basketAmount} шт.\n");
        }
        public Order userPay = new Order();
        private void Button_Clicked(object sender, EventArgs e)   // разобраться чтобы не заходило по сто раз
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            prod.Clear();
            ProductBasket.list.Clear();
            DbWorking.ViewToBasket(1);
            DbWorking.DeleteBasket();
            string checkList = String.Empty;
            int i = 0;
            foreach (String txt in prod)
            {
                checkList = checkList +"\n"+ $"{++i}. " + txt;
            }

                userPay.total = ProductBasket.prCheck;
                userPay.firstName = entry5.Text;
                userPay.phone = entry2.Text;
                userPay.products = checkList;
                userPay.point = 2; 

            DisplayActionSheet("Кондитер", "Ок", null, "Ожидайте, пока ваш заказ примет наш оператор.");
            DbWorking.OrdersAdd(userPay); 

            Application.Current.MainPage = new UserMenu(0);
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            CreateForm(1);
        }

        private void Button1_Clicked(object sender, EventArgs e) // доставка
        {
            CreateForm(2);
        }

        public Entry entry5 = new Entry // name
        {
            FontSize = 14,
            FontFamily = "Ubuntu-Light.ttf#Ubuntu",
            Keyboard = Keyboard.Text,
            IsReadOnly = true
        };

        public Entry entry2 = new Entry // telephone
        {
            FontSize = 14,
            FontFamily = "Ubuntu-Light.ttf#Ubuntu",
            Keyboard = Keyboard.Text,
            IsReadOnly = true
        };
        public void CreateForm(int check)
        {
            userPay.GPS = "Минск, Леонида-Беды, 4";
            userPay.nomination = "Самовывоз";
            Frame frame = new Frame
            {
                CornerRadius = 8,
                HasShadow = false,
                BorderColor = Color.FromHex("#E8E8E8"),
                Padding = 0,
                HeightRequest = 33,
            };

            Button button1 = new Button
            {
                Padding = new Thickness(30, 0, 0, 0),
                WidthRequest = 162,
                BackgroundColor = Color.FromHex("#0000ffff"),
                TextTransform = TextTransform.None,
                Text = "Доставка",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Medium.ttf#Ubuntu",
                FontSize = 16
            };

            button1.Clicked += Button1_Clicked;

            Button button2 = new Button
            {
                Padding = new Thickness(-30, 0, 0, 0),
                Margin = new Thickness(-5, 0, 0, 0),
                WidthRequest = 162,
                BackgroundColor = Color.FromHex("#b39afd"),
                TextTransform = TextTransform.None,
                Text = "Самовывоз",
                TextColor = Color.White,
                FontFamily = "Ubuntu-Medium.ttf#Ubuntu",
                FontSize = 16
            };

            button2.Clicked += Button2_Clicked;

            if(check == 2)
            {
                button1.BackgroundColor = Color.FromHex("#b39afd");
                button1.TextColor = Color.White;
                button2.BackgroundColor = Color.FromHex("#0000ffff");
                button2.TextColor = Color.Black;
            }

            StackLayout stackLayout2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    button1,
                    button2
                }
            };

            frame.Content = stackLayout2;



            Frame frame3 = new Frame
            {
                Padding = new Thickness(0, 0, 0, 0),
                CornerRadius = 8,
                HeightRequest = 39,
                HasShadow = false,
                BorderColor = Color.FromHex("#E8E8E8")
            };

            frame3.Content = entry2;

            entry2.Focused += (object sender, FocusEventArgs e) =>
            {
                frame3.BorderColor = Color.LightSkyBlue;
            };
            entry2.Unfocused += (object sender, FocusEventArgs e) =>
            {
                frame3.BorderColor = Color.FromHex("#E8E8E8");
            };

            RadioButton radioButton = new RadioButton
            {
                Content = "Налич.",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                FontSize = 16
            };
            RadioButton radioButton2 = new RadioButton
            {
                Content = "Картой",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                FontSize = 16
            };

            radioButton.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;

            RadioButton radioButton3 = new RadioButton
            {
                Content = "Картой\nонлайн",
                TextColor = Color.FromHex("#E8E8E8"),
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                FontSize = 16,
                IsEnabled = false
            };

            StackLayout stack1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 35,
                Margin = new Thickness(-10,0,0,15),
                Children =
                {
                    radioButton,
                    radioButton2,
                    radioButton3
                }
            };

            Frame frame4 = new Frame
            {
                Padding = new Thickness(0),
                CornerRadius = 8,
                HeightRequest = 39,
                BorderColor = Color.FromHex("#E8E8E8"),
                HasShadow = false
            };

            Picker picker = new Picker
            {
                TitleColor = Color.LightGray,
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                FontSize = 14,
                Items =
                {
                "20 мин.", "25 мин.", "30 мин.", "35 мин.", "40 мин.", "45 мин."
                },
            };

            picker.SelectedIndexChanged += (object sender, EventArgs e) =>// НЕ РАБОТАЕТ!
            {
               userPay.time = picker.Items[picker.SelectedIndex];
            };


            frame4.Content = picker;

            picker.Focused += (object sender, FocusEventArgs e) =>
            {
                frame4.BorderColor = Color.LightSkyBlue;
            };
            picker.Unfocused += (object sender, FocusEventArgs e) =>
            {
                frame4.BorderColor = Color.FromHex("#E8E8E8");
            };

            Frame frame8 = new Frame
            {
                Padding = new Thickness(0),
                CornerRadius = 8,
                HeightRequest = 39,
                BorderColor = Color.FromHex("#E8E8E8"),
                HasShadow = false,
                Margin = new Thickness(0)
            };
            
            Editor editor = new Editor
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 14,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                Keyboard = Keyboard.Text,
                MaxLength = 100,
                IsTabStop = false,
                AutoSize = EditorAutoSizeOption.TextChanges,
                TextColor = Color.Black
            };

            editor.TextChanged += Editor_TextChanged;

            frame8.Content = editor;

            editor.Focused += (object sender, FocusEventArgs e) =>
            {
                frame8.BorderColor = Color.LightSkyBlue;
            };
            editor.Unfocused += (object sender, FocusEventArgs e) =>
            {
                frame8.BorderColor = Color.FromHex("#E8E8E8");
            };

            Frame frame6 = new Frame
            {
                Padding = new Thickness(0),
                CornerRadius = 8,
                HasShadow = false,
                BorderColor = Color.FromHex("#E8E8E8"),
                Margin = new Thickness(0, 15, 0, 0)
            };

            frame6.Content = entry5;

            entry5.Focused += (object sender, FocusEventArgs e) =>
            {
                frame6.BorderColor = Color.LightSkyBlue;
            };
            entry5.Unfocused += (object sender, FocusEventArgs e) =>
            {
                frame6.BorderColor = Color.FromHex("#E8E8E8");
            };

            Frame frame7 = new Frame
            {
                BackgroundColor = Color.FromHex("#F8F8F8"),
                Padding = new Thickness(20, 10, 20, 10),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HasShadow = false
            };

            Button button = new Button
            {
                FontSize = 16,
                FontFamily = "Ubuntu-Bold.ttf#Ubuntu",
                Text = $"Оформить заказ на {ProductBasket.buttonText} р.\nДоставка = 0 р.",
                TextTransform = 0,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#b39afd"),
                CornerRadius = 8,
                HeightRequest = 60
            };

            button.Clicked += Button_Clicked;

            frame7.Content = button;

            StackLayout stack = new StackLayout 
            {
                Margin = new Thickness(30, 10, 30, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            stack.Children.Add(frame);

            
            if (check == 2)
            {
                Frame frame2 = new Frame
                {
                    Padding = new Thickness(0),
                    CornerRadius = 8,
                    HasShadow = false,
                    BorderColor = Color.FromHex("#E8E8E8"),
                    Margin = new Thickness(0, 15, 0, 0)
                };

                Entry entry = new Entry
                {
                    FontSize = 14,
                    FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                    Keyboard = Keyboard.Text,
                    Placeholder = "Минск, улица, дом"
                };

                frame2.Content = entry;

                entry.Focused += (object sender, FocusEventArgs e) =>
                {
                    frame2.BorderColor = Color.LightSkyBlue;
                };
                entry.Unfocused += (object sender, FocusEventArgs e) =>
                {
                    frame2.BorderColor = Color.FromHex("#E8E8E8");
                };

                stack.Children.Add(frame2);

                stack.Children.Add(new Frame
                {
                    Padding = new Thickness(10),
                    HasShadow = false,
                    Margin = new Thickness(12, -55, 208, 0), //te
                    BackgroundColor = Color.White
                });
                stack.Children.Add(new Label
                {
                    Text = "Адрес доставки",
                    TextColor = Color.Black,
                    FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                    Margin = new Thickness(13,-24,0,30)
                });
                entry.TextChanged += Entry_TextChanged;
                userPay.nomination = "Доставка по адресу";

            }

            stack.Children.Add(frame6);
            stack.Children.Add(new Frame
            {
                Padding = new Thickness(10),
                Margin = new Thickness(12, -52, 285, 0),
                BackgroundColor = Color.White,
                HasShadow = false
            });
            stack.Children.Add(new Label
            {
                Text = "Имя",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                Margin = new Thickness(13, -27, 0, 45)
            });

            stack.Children.Add(frame3);
            stack.Children.Add(new Frame
            {
                Padding = new Thickness(10),
                Margin = new Thickness(12, -55, 198, 0),
                BackgroundColor = Color.White,
                HasShadow = false
            });
            stack.Children.Add(new Label
            {
                Text = "Номер телефона",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                Margin = new Thickness(13, -26, 200, 45),
            });

            stack.Children.Add(stack1);
            stack.Children.Add(frame4);
            stack.Children.Add(new Frame
            {
                Padding = new Thickness(10),
                HasShadow = false,
                Margin = new Thickness(12, -55, 100, 0),
                BackgroundColor = Color.White
            });

            stack.Children.Add(new Label // сделать picker
            {
                Text = "Через сколько минут заберете?",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                Margin = new Thickness(13, -26, 0, 50)
            });

            stack.Children.Add(frame8);
            stack.Children.Add(new Frame
            {
                Padding = new Thickness(10),
                HasShadow = false,
                Margin = new Thickness(12, -64, 162, 0),
                BackgroundColor = Color.White
            });
            stack.Children.Add(new Label
            {
                Text = "Комментарий к заказу",
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Regular.ttf#Ubuntu",
                Margin = new Thickness(13, -17, 0, 50)
            });

            StackLayout stackLayout = new StackLayout
            {
                Children =
                {
                    new Line
                    {
                    X1 = 500,
                    Stroke = Brush.LightGray,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(0,0,0,10)
                    },
                    stack,
                    frame7
                }
            };

            Content = stackLayout;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            userPay.GPS = e.NewTextValue;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            userPay.comment = e.NewTextValue;
        }

        private void RadioButton2_CheckedChanged(object sender, CheckedChangedEventArgs e) // карта
        {
            userPay.payment = "Картой";
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e) // наличн
        {
            userPay.payment = "Наличными";
        }
    }
}