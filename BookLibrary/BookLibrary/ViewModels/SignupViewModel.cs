using BookLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        private string _name, _email, _password;
        public Command SignupCommand { get; }
        public Command SigninViewComand { get; }
        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        public string Email
        {
            set
            {
                _email = value;
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
            }
            get
            {
                return _password;
            }
        }
        public SignupViewModel()
        {
            Email = Preferences.Get("lastEmail", "");
            SignupCommand = new Command(onSignupClicked, canExecute);
            SigninViewComand = new Command(onSigninClicked, canExecute);
        }
        private bool canExecute()
        {
            return !IsBusy;
        }
        private async void onSignupClicked()
        {
            IsBusy = true;
            var res = await UserService.Signup(Name, Email, Password);
            IsBusy = false;
            await Application.Current.MainPage.DisplayAlert("Noti", res.Item2, "OK");
            if (res.Item1) await Shell.Current.GoToAsync("//SigninPage");
        }
        private async void onSigninClicked()
        {
            await Shell.Current.GoToAsync("//SigninPage");
        }
    }
}
