using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

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

            var user = new Models.User()
            {
                Username = EntryUsername,
                Password = EntryPassword,
                Email = EntryEmail
            };

            var res = await UserDb.RegisterUser(user);
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

