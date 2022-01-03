using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Erme.Model;

namespace Erme
{
    public partial class App : Application
    {

        public static ErmeModel m = new Model.ErmeModel();
        public static SpentModel sp = new Model.SpentModel();

        public NavigationPage NavPage { get; private set; }

        public App()
        {
            InitializeComponent();

            NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
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
    }
}
