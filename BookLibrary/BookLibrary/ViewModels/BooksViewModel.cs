using BookLibrary.Models;
using BookLibrary.Services;
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
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
