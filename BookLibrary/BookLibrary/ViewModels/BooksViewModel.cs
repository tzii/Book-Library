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
    class BooksViewModel : BaseViewModel
    {
        private List<Book> books;
        public Command LoadPage { get; }
        public Command<Book> BookClicked { get; }
        public List<Book> Books
        {
            get { return books; }
            set
            {
                SetProperty(ref books, value);
            }
        }
        public BooksViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            BookClicked = new Command<Book>(onBookClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                Books = await BookService.getAll();
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
