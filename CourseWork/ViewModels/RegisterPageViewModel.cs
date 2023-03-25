using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Helpers;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class RegisterPageViewModel : BaseViewModel
	{
		[ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string _entryUsername;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string _entryEmail;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        string _entryPassword;

        public RegisterPageViewModel(IAppState appState, IUserDatabaseService userDb) : base(appState, userDb)
        {
		}

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private async Task Register()
        {

            User user = null;
            if (EntryEmail.Contains("@") && EntryEmail.Contains("."))
            {
                user = new User()
                {
                    Username = EntryUsername,
                    Password = Password.Hash(EntryPassword),
                    Email = EntryEmail
                };
                
            }
            else
            {
                if (Application.Current != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Email", $"Please enter a valid email", "OK");
                }
                return;
            }
            
            if (!await UserDb.CheckIfUser(user))
            {
                var res = await UserDb.RegisterUser(user);
                if(res != 0)
                {
                    await Shell.Current.GoToAsync("//LoginPage");
                }
            }
            else
            {
                if (Application.Current != null)
                {
                    await Application.Current.MainPage.DisplayAlert("User Exists", $"{EntryUsername} is already a user", "OK");
                }
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

