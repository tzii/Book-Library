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
    public partial class BookDetailPage : ContentPage
    {
        private bool firstLoad;
        private BookDetailViewModel _model;
        public BookDetailPage()
        {
            InitializeComponent();
            BindingContext = _model = new BookDetailViewModel();
            firstLoad = true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (firstLoad)
            {
                _model.OnAppearing();
                firstLoad = false;
            }
        }
    }
}