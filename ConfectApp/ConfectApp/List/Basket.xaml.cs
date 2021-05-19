using ConfectApp.Menu;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        public Basket()
        {
            ProductBasket.prodlist.Clear();
            ProductBasket.sumlist.Clear();
            ProductBasket.list.Clear();

            InitializeComponent();

            Product p = DbWorking.ViewToBasket(0);

            if (p == null) { ProdEmpty(); }
            else { ProdToBasket(); }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int check = DbWorking.CheckIO();
            if (check == 0)
            {
                Application.Current.MainPage = new GuestMenu(0);
            }
            else
            {
                Application.Current.MainPage = new UserMenu(0);
            }
        }

        public void ProdToBasket()
        {
            Frame frame = new Frame
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

            button.Clicked += (object sender, EventArgs e) =>
            {
                Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
                int check = DbWorking.CheckIO();
                if (check == 0)
                {
                    DisplayActionSheet("Кондитер", "Ок", null, "Ошибка! Для оплаты товара, необходимо зайти в учетную запись.");
                    return;
                }
                Navigation.PushAsync(new BasketList());
            };

            frame.Content = button;

            StackLayout st = new StackLayout
            {
                Spacing = 0,
            };

            foreach (Grid grid in ProductBasket.list)
            {
                st.Children.Add(grid);
                st.Children.Add(new Line
                {
                    Margin = new Thickness(0, 10, 0, 0),
                    HeightRequest = 0.2,
                    X1 = 400,
                    Stroke = Brush.LightGray,
                    HorizontalOptions = LayoutOptions.Center
                });
            }

            StackLayout stackLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Children =
                                {
                            st
                                }
            };

            ScrollView scrollView = new ScrollView { Content = stackLayout };

            StackLayout stack = new StackLayout
            {
                BackgroundColor = Color.White,
                Spacing = 0,
                Children =
                                {
                            scrollView,
                            frame
                                }
            };
            Content = stack;
        }
        public void ProdEmpty()
        {
            Button Button1 = new Button
            {
                HeightRequest = 40,
                WidthRequest = 220,
                Margin = new Thickness(0, 15, 0, 0),
                Text = "Перейти в меню",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 8,
                BackgroundColor = Color.FromHex("#b39afd"),
                TextColor = Color.White,
                TextTransform = TextTransform.None,
                FontSize = 16,
                FontFamily = "Ubuntu-Bold.ttf#Ubuntu"
            };
            Button1.Clicked += Button_Clicked;

            Content = new StackLayout
            {
                Margin = new Thickness(20, -150, 0, 0),
                Padding = new Thickness(50, 0, 50, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Children = {
                                    new Image { Source = "icon.png", Aspect = Aspect.AspectFill, Margin = new Thickness(10, 0, 10, -50) },
                                    new Label { Text = "Упс, в корзине пусто!", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, FontSize = 20, TextColor = Color.Black, FontFamily = "Ubuntu-Regular.ttf#Ubuntu" },
                                    new Label { Margin = new Thickness(0, 5, 0, 0), Text = "Не паникуйте, просто \nоткройте меню и добавьте то, \nчто вам нравится!", HorizontalTextAlignment = TextAlignment.Center, FontFamily = "Ubuntu-Light.ttf#Ubuntu",
                                    FontSize = 15, TextColor = Color.FromHex("#808080")},
                                    Button1
                                            }
            };
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e) // дублирование кода
        {
            Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
            DbWorking.DeleteBasket();
            ProductBasket.list.Clear();
            ProdEmpty();
        }
    }
}



//Vibration.Vibrate(TimeSpan.FromSeconds(0.03));
//await Navigation.PushAsync(new BasketList());