using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using BookLibrary.Services;
using System.Threading.Tasks;

namespace BookLibrary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(BookName), nameof(BookName))]
    public class ChapterDetailViewModel: BaseViewModel
    {
        private bool loadFirst;
        private string id, bookName, name, content,nameView;
        private int number;
        public Command LoadPage { get; }
        public Command BackClicked { get; }
        public string Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                load(value);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                NameView = String.Format("Chương {0}: {1}", Number, value);
            }
        }
        public string NameView
        {
            get => nameView;
            set => SetProperty(ref nameView, value);
        }
        public int Number
        {
            get => number;
            set
            {
                SetProperty(ref number, value);
                NameView = String.Format("Chương {0}: {1}", value, Name);
            }
        }
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        public string BookName
        {
            get => bookName;
            set => SetProperty(ref bookName, value);
        }
        public ChapterDetailViewModel()
        {
            loadFirst = false;
            LoadPage = new Command(excuteLoadPageCommand);
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
        private async void load(string i)
        {
            await loadInfo(i);
            loadFirst = true;
            IsBusy = false;
        }
        private async Task loadInfo(string i)
        {
            var chapter = await ChapterService.getChapterbyId(i);
            Name = chapter.name;
            Number = chapter.number;
            Content = chapter.content;
        }
        private async void onBackClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
    
}
