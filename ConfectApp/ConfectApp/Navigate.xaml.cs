using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfectApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigate : TabbedPage
    {
        public Navigate(int check)
        {
            InitializeComponent();
            if (check == 1)
            {
                CurrentPage = Children[0];
            }
            else if (check == 2)
            {
                CurrentPage = Children[1];
            }
            else if (check == 3)
            {
                CurrentPage = Children[3];
            }
            else
            {
                CurrentPage = Children[2];
            }
        }
    }
}