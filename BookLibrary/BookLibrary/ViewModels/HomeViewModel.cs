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
    public class HomeViewModel : BaseViewModel
    {
        private List<Book> recommended, newBooks;
        private List<Category> topCategories;
        private string name;
        public Command LoadPage { get; }
        public Command GotoAllBookPage { get; }
        public Command GotoCategoriesPage { get; }
        public Command<Category> CategoryClicked { get; }
        public Command<Book> BookClicked { get; }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public List<Book> Recommended
        {
            get { return recommended; }
            set
            {
                SetProperty(ref recommended, value);
            }
        }
        public List<Book> NewBooks
        {
            get { return newBooks; }
            set
            {
                SetProperty(ref newBooks, value);
            }
        }

        public List<Category> TopCategories
        {
            get { return topCategories; }
            set
            {
                SetProperty(ref topCategories, value);
            }
        }
        public HomeViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            GotoAllBookPage = new Command(async () => await Shell.Current.GoToAsync("//BooksPage"));
            GotoCategoriesPage = new Command(async () => await Shell.Current.GoToAsync("//CategoriesPage"));
            CategoryClicked = new Command<Category>(onCategoryClicked);
            BookClicked = new Command<Book>(onBookClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                Name = UserService.Name;
                Recommended = await BookService.getRecommended();
                NewBooks = await BookService.getNewBooks();
                TopCategories = await CategoryService.getTop();
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
            await Shell.Current.GoToAsync($"{nameof(CategoryDetailPage)}?{nameof(CategoryDetailViewModel.Id)}={category._id}&{nameof(CategoryDetailViewModel.Name)}={category.name}&{nameof(CategoryDetailViewModel.Image)}={category.image}&{nameof(CategoryDetailViewModel.Description)}={category.description}");
        }
        private async void onBookClicked(Book book)
        {
            if (book == null) return;
            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.Id)}={book._id}&{nameof(BookDetailViewModel.Name)}={book.name}&{nameof(BookDetailViewModel.Image)}={book.image}&{nameof(BookDetailViewModel.Price)}={book.price}");
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
