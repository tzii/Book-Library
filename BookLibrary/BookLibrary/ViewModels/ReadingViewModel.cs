using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BookLibrary.Services;
using Xamarin.Forms;
using BookLibrary.Models;
using BookLibrary.Views;

namespace BookLibrary.ViewModels
{
    public class ReadingViewModel: BaseViewModel
    {
        private List<Book> lastRead,own;
        public Command LoadPage { get; }
        public Command<Book> BookClicked { get; }
        public List<Book> LastRead
        {
            get => lastRead;
            set => SetProperty(ref lastRead, value);
        }
        public List<Book> Own
        {
            get => own;
            set => SetProperty(ref own, value);
        }
        public ReadingViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            BookClicked = new Command<Book>(onBookClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                LastRead = await BookService.getLastRead();
                Own = await BookService.getOwnBooks();
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
        private async void onBookClicked(Book book)
        {
            if (book == null) return;
            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.Id)}={book._id}&{nameof(BookDetailViewModel.Name)}={book.name}&{nameof(BookDetailViewModel.Image)}={book.image}&{nameof(BookDetailViewModel.Price)}={book.price}");
        }

    }
}
