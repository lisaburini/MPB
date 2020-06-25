using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPB
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    //public partial class MainPage : ContentPage
    public partial class MainPage : TabbedPage
    {
        //IAuth auth;

        public MainPage()
        {
            InitializeComponent();
            var navigationPage1 = new NavigationPage(new HomePage());
            navigationPage1.IconImageSource = ImageSource.FromFile("home.png");
            //navigationPage1.Title = "Home";

            var navigationPage2 = new NavigationPage(new TransactionsPage());
            navigationPage2.IconImageSource = ImageSource.FromFile("money.png");
            //navigationPage2.Title = "Transactions";

            var navigationPage3 = new NavigationPage(new StatisticsPage());
            navigationPage3.IconImageSource = ImageSource.FromFile("statistics.png");
            //navigationPage3.Title = "Statistics";

            var navigationPage4 = new NavigationPage(new SectionProfilePage());
            navigationPage4.IconImageSource = ImageSource.FromFile("profile.png");
            //navigationPage4.Title = "Profile";

            Children.Add(navigationPage1);
            Children.Add(navigationPage2);
            Children.Add(navigationPage3);
            Children.Add(navigationPage4);

        }

    }
}
