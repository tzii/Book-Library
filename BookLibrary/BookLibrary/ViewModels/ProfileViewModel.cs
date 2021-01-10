using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using BookLibrary.Services;

namespace BookLibrary.ViewModels
{
    public class ProfileViewModel :BaseViewModel
    {
        private string name, email;
        private int balance;
        public Command LoadPage { get; }
        public Command LogoutClicked { get; }
        public string Name
        {
            get => name;
            set=> SetProperty(ref name, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public int Balance
        {
            get => balance;
            set => SetProperty(ref balance, value);
        }
        public ProfileViewModel()
        {
            LoadPage = new Command(excuteLoadPageCommand);
            LogoutClicked = new Command(exciteLogoutClicked);
        }
        private void excuteLoadPageCommand()
        {
            try
            {
                Name = UserService.Name;
                Balance = UserService.Balance;
                Email = UserService.Email;
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
        private async void exciteLogoutClicked()
        {
            var res = await UserService.Logout();
            if (res) await Shell.Current.GoToAsync("//SigninPage");
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
