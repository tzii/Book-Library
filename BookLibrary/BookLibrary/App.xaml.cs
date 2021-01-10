using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookLibrary.Services;

namespace BookLibrary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            HTTPService.Settings();
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
