using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

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

        public LoginPageViewModel(IAppState appState): base(appState)
        {
            
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            //check credentials
            appState.CurrentUser = new Models.User()
            {
                Username = EntryUsername,
                Password = EntryPassword
            };
            await Shell.Current.GoToAsync("//CreateProgrammePage");
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(EntryUsername) && !string.IsNullOrEmpty(EntryPassword);
        }
    }
}

