using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MPB
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetCultureToUSEnglish();

            if (DependencyService.Get<IAuth>().IsUserLoggedIn())
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new NavigationPage(new IntroPage());
            }


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }
    }
}
