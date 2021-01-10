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
    public partial class CategoryDetailPage : ContentPage
    {
        private CategoryDetailViewModel _model;
        public CategoryDetailPage()
        {
            InitializeComponent();
            BindingContext = _model = new CategoryDetailViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _model.OnAppearing();
        }
    }
}