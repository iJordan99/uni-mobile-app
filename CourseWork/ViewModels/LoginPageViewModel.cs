using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class LoginPageViewModel : BaseViewModel
	{
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        string entryUsername;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        string entryPassword;

        public LoginPageViewModel(IAppState appState, IUserDatabaseService userDB): base(appState, userDB)
        {

        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {

            var user = new User()
            {
                Username = EntryUsername,
                Password = EntryPassword
            };

            User validatedUser = await userDB.ValidateUser(user);

            if (validatedUser != null)
            {
                appState.CurrentUser = validatedUser;
                await Shell.Current.GoToAsync("//HomePage");
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

