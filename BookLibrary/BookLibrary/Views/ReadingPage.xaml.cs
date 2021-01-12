using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadingPage : ContentPage
    {
        private ReadingViewModel _model;
        public ReadingPage()
        {
            InitializeComponent();
            BindingContext = _model = new ReadingViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _model.OnAppearing();
        }
    }
}