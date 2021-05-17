using ConfectApp.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp.List
{
    static class ProductBasket
    {
        public static int prCheck;
        public static string buttonText;
        
        public static List<Int32> sumlist = new List<Int32>();

        public static List<String> prodlist = new List<String>();  
        public static List<Grid> list = new List<Grid>();

        public static void Prod(Product p)
        {
            Image image = new Image
            {
                Source = p.productPhoto,
                HeightRequest = 110,
                WidthRequest = 150,
                HorizontalOptions = LayoutOptions.Start,
            };
            Label labelName = new Label
            {
                Margin = new Thickness(-20, 4, 0, 0),
                Text = p.productName,
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Bold.ttf#Ubuntu",
                FontSize = 18
            };
            Label labelCharacter = new Label
            {
                Margin = new Thickness(-20, 30, 0, 0),
                Text = p.productCharacter,
                TextColor = Color.FromHex("#8B8B8B"),
                FontSize = 13,
                FontFamily = "Ubuntu-Light.ttf#Ubuntu"
            };
            Label labelCol = new Label
            {
                Text = p.basketAmount.ToString(),
                Margin = new Thickness(-3, 16, 0, 0),
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Bold.ttf#Ubuntu",
                FontSize = 17
            };
            Label labelPrice = new Label
            {
                Text = p.productPrice.ToString() + " р.",
                Margin = new Thickness(0, 16, 0, 0),
                TextColor = Color.Black,
                FontFamily = "Ubuntu-Bold.ttf#Ubuntu",
                FontSize = 17
            };
            Image imageMin = new Image
            {
                Source = "minus.png",
                HeightRequest = 45,
                WidthRequest = 45
            };
            Image imagePlus = new Image
            {
                Source = "plus.png",
                HeightRequest = 45,
                WidthRequest = 45,
                Margin = new Thickness(-3, 0, 0, 0)
            };

            Grid grid = new Grid
            {
                Padding = 0,
                Margin = new Thickness(15, 15, 0, 0)
            };

            Int32 productPrice = new Int32();
            productPrice = p.productPrice * Int32.Parse(labelCol.Text);
            Int32 productId = new Int32();
            productId = p.productId; 

            labelPrice.Text = productPrice.ToString() + " р.";

            int price = Int32.Parse(labelPrice.Text.Replace("р.", " ")); 
            sumlist.Add(price);

            int priceCheck = 0; 
            foreach (Int32 sum in sumlist)      
            {
                priceCheck = priceCheck + sum;
            }

            buttonText = $"{priceCheck}";

            prCheck = priceCheck;

            var TapGestureRecognizer = new TapGestureRecognizer();
            TapGestureRecognizer.Tapped += (object sender, EventArgs e) =>
            {
                if (Int32.Parse(labelCol.Text) != 0)
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
                    labelPrice.Text = Convert.ToString(Int32.Parse(labelPrice.Text.Replace("р.", " ")) - productPrice) + " р.";
                    labelCol.Text = Convert.ToString(Int32.Parse(labelCol.Text) - 1);

                    if (labelCol.Text == "0")
                    {
                        list.Remove(grid);
                        DbWorking.DeleteBasketIndex(labelName.Text);
                        Application.Current.MainPage = new UserMenu(3);
                    }
                    else
                    {
                        DbWorking.ManipulationBasket(productId, 0); 
                        Application.Current.MainPage = new UserMenu(3);
                    }
                }
            };

            imageMin.GestureRecognizers.Add(TapGestureRecognizer);

            var TapGestureRecognizer_1 = new TapGestureRecognizer();
            TapGestureRecognizer_1.Tapped += (object sender, EventArgs e) =>
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(2));
                labelPrice.Text = Convert.ToString(Int32.Parse(labelPrice.Text.Replace("р.", " ")) + productPrice) + " р.";
                labelCol.Text = Convert.ToString(Int32.Parse(labelCol.Text) + 1);

                DbWorking.ManipulationBasket(productId, 1);
                Application.Current.MainPage = new UserMenu(3);
            };
            imagePlus.GestureRecognizers.Add(TapGestureRecognizer_1);


            grid.Children.Add(image, 0, 0);
            grid.Children.Add(labelName, 1, 0);
            grid.Children.Add(labelCharacter, 1, 0);
            grid.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 0,
                Margin = new Thickness(-28, 55, 0, 0),
                Spacing = 0,

                Children =
                {
                    imageMin,
                    labelCol,
                    imagePlus,
                    labelPrice
                }

            }, 1, 0);

            list.Add(grid);
        }
    }
}