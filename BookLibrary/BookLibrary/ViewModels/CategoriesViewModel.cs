using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    class CategoriesViewModel : BaseViewModel
    {
        private List<Category> categories;
        public Command LoadPage { get; }
        public Command<Category> CategoryClicked { get; }
        public List<Category> Categories
        {
            get { return categories; }
            set
            {
                SetProperty(ref categories, value);
            }
        }
        public CategoriesViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            CategoryClicked = new Command<Category>(onCategoryClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                Categories = await CategoryService.getAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void onCategoryClicked(Category category)
        {
            if (category == null) return;
            Debug.WriteLine($"{nameof(CategoryDetailPage)}?{nameof(CategoryDetailViewModel.Id)}={category._id}");
            await Shell.Current.GoToAsync($"{nameof(CategoryDetailPage)}?{nameof(CategoryDetailViewModel.Id)}={category._id}&{nameof(CategoryDetailViewModel.Name)}={category.name}&{nameof(CategoryDetailViewModel.Image)}={category.image}&{nameof(CategoryDetailViewModel.Description)}={category.description}");
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
