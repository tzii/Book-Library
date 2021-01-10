using BookLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class SigninViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        public Command SigninCommand { get; }
        public Command SignupViewComand { get; }
        public string Email
        {
            set
            {
                _email = value;
                //Preferences.Set("lastEmail", value);
            }
            get
            {
                return _email;
            }
        }
        public string Password
        {
            set
            {
                _password = value;
                //Preferences.Set("lastEmail", value);
            }
            get
            {
                return _password;
            }
        }
        public SigninViewModel()
        {
            Email = Preferences.Get("lastEmail", "");
            SigninCommand = new Command(onSigninClicked, canExecute);
            SignupViewComand = new Command(onSignupClicked, canExecute);
        }
        private bool canExecute()
        {
            return !IsBusy;
        }
        private async void onSigninClicked()
        {
            IsBusy = true;
            var res = await UserService.Login(Email, Password);
            if (res.Item1)
            {
                await UserService.LoadInfo();
                IsBusy = false;
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Alert", res.Item2, "Ok");
            }
        }
        private async void onSignupClicked()
        {
            await Shell.Current.GoToAsync("//SignupPage");
        }
    }
}
