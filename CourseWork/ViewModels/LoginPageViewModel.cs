using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Helpers;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class LoginPageViewModel : BaseViewModel
	{
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        string _entryUsername;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        string _entryPassword;

        public LoginPageViewModel(IAppState appState, IUserDatabaseService userDb): base(appState, userDb)
        {

        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {

            var user = new User
            {
                Username = EntryUsername,
                Password = Password.Hash(EntryPassword)
            };

            User validatedUser = await UserDb.ValidateUser(user);

            if (validatedUser != null)
            {
                AppState.CurrentUser = validatedUser;
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                if (Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Credentials", "Try Again", "OK");
                }
            }
            
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(EntryUsername) && !string.IsNullOrEmpty(EntryPassword);
        }

        [RelayCommand]
        private async Task NavigateToRegisterPage()
        {
            await Shell.Current.GoToAsync("/RegisterPage");
        }
    }
}

