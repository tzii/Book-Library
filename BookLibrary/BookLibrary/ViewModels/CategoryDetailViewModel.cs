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
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Image), nameof(Image))]
    [QueryProperty(nameof(Description), nameof(Description))]
    public class CategoryDetailViewModel : BaseViewModel
    {
        private string id, name, image, description;
        private List<Book> books;
        public Command LoadPage { get; }
        public Command BackClicked { get; }
        public Command<Book> BookClicked { get; }
        public string Id 
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public List<Book> Books
        {
            get => books;
            set => SetProperty(ref books, value);
        }
        public CategoryDetailViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            BookClicked = new Command<Book>(onBookClicked);
            BackClicked = new Command(onBackClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                Books = await CategoryService.getCategory(Id);
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
        private async void onBackClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
