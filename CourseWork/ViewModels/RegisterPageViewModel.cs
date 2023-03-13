using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
	public partial class RegisterPageViewModel : BaseViewModel
	{
		[ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string entryUsername;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string entryEmail;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string entryPassword;

        public RegisterPageViewModel(IAppState appState, IUserDatabaseService userDB) : base(appState, userDB)
        {
		}

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private async Task Register()
        {

            Models.User user = new Models.User()
            {
                Username = EntryUsername,
                Password = EntryPassword,
                Email = EntryEmail
            };

            var res = await userDB.RegisterUser(user);
            if(res != 0)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        private bool CanRegister()
        {
            return !string.IsNullOrEmpty(EntryUsername)
                && !string.IsNullOrEmpty(EntryPassword)
                && !string.IsNullOrEmpty(EntryEmail);
        }
    }
}

