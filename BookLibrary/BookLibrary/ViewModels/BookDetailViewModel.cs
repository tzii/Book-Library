using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using BookLibrary.Services;
using BookLibrary.Models;
using System.Threading.Tasks;

namespace BookLibrary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Image), nameof(Image))]
    [QueryProperty(nameof(Price), nameof(Price))]
    class BookDetailViewModel : BaseViewModel
    {
        private bool loadFirst;
        private string id, name, image, description, author;
        private int price;
        private List<Chapter> chapters;
        private bool owned, canBuy;
        public Command LoadPage { get; }
        public Command BuyClicked { get; }
        public Command BackClicked { get; }
        public Command<Chapter> ChapterClicked { get; }
        public string Id
        {
            get=> id;
            set 
            {
                SetProperty(ref id, value);
                load(value);
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
        public List<Chapter> Chapters
        {
            get => chapters;
            set => SetProperty(ref chapters, value);
        }
        public string Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }
        public int Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public bool Owned
        {
            get => owned;
            set
            {
                SetProperty(ref owned, value);
                CanBuy = !value;
            }
        }
        public bool CanBuy
        {
            get => canBuy;
            set => SetProperty(ref canBuy, value);
        }
        public BookDetailViewModel()
        {
            loadFirst = false;
            LoadPage = new Command(excuteLoadPageCommand);
            BuyClicked = new Command(excuteBuyClicked);
            ChapterClicked = new Command<Chapter>(excuteChapterClicked);
            BackClicked = new Command(onBackClicked);
        }
        private async void excuteLoadPageCommand()
        {
            try
            {
                if (loadFirst) await loadInfo(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                if (loadFirst) IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async Task loadInfo(string i)
        {
            var book = await BookService.getBookById(i);
            Name = book.name;
            Author = book.author;
            Image = book.image;
            Description = book.description;
            Price = book.price;
            Owned = UserService.Books.Contains(i);
            Chapters = await BookService.getAllChaptersByBookId(i);
        }
        private async void load(string i)
        {
            await loadInfo(i);
            loadFirst = true;
            IsBusy = false;
        }
        private async void excuteBuyClicked()
        {
            var dialog = await Shell.Current.DisplayAlert("Thông Báo!", "Bạn có muốn mua sách không?", "Có", "Không");
            if (!dialog) return;
            var res = await UserService.BuyBook(Id);
            if (!res.Item1) await Shell.Current.DisplayAlert("Thông Báo!", "Mua sách không thành công!", "OK");
            else
            {
                await Shell.Current.DisplayAlert("Thông Báo!", "Bạn đã mua sách thành công!", "OK");
                IsBusy = true;
            }
         }
        private async void excuteChapterClicked(Chapter chapter) 
        {
            if (chapter == null) return;
            if (!Owned) await Shell.Current.DisplayAlert("Thông Báo!", "Bạn chưa sở hữu quyển sách này.", "OK");
            await Shell.Current.DisplayAlert("Thông Báo!", chapter._id, "OK");
        }
        private async void onBackClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
