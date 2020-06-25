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

    
    public partial class MainPage : TabbedPage
    {
       
        public MainPage()
        {
            InitializeComponent();
            var navigationPage1 = new NavigationPage(new HomePage());
            navigationPage1.IconImageSource = ImageSource.FromFile("home.png");
           

            var navigationPage2 = new NavigationPage(new TransactionsPage());
            navigationPage2.IconImageSource = ImageSource.FromFile("money.png");
            

            var navigationPage3 = new NavigationPage(new StatisticsPage());
            navigationPage3.IconImageSource = ImageSource.FromFile("statistics.png");
           

            var navigationPage4 = new NavigationPage(new SectionProfilePage());
            navigationPage4.IconImageSource = ImageSource.FromFile("profile.png");

            Children.Add(navigationPage1);
            Children.Add(navigationPage2);
            Children.Add(navigationPage3);
            Children.Add(navigationPage4);

        }

    }
}
