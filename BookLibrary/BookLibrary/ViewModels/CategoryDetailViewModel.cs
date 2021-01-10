using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BookLibrary.Services;
using Xamarin.Forms;
using BookLibrary.Models;

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
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
