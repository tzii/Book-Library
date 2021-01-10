using BookLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }
        private async void init()
        {
            if (await UserService.LoadInfo()) await Shell.Current.GoToAsync("//HomePage");
            else await Shell.Current.GoToAsync("//SigninPage");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            init();
        }
    }
}