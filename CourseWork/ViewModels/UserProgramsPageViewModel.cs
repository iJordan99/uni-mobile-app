using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
    public partial class UserProgramsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Models.Program> _programs;

        private readonly IProgramDatabaseService _programDb;
       
        public UserProgramsPageViewModel(IAppState appState, IUserDatabaseService userDb, IProgramDatabaseService programDb) : base(appState, userDb)
        {
            this._programDb = programDb;
            Programs = new ObservableCollection<Models.Program>();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetPrograms();
        }

        private async Task GetPrograms()
        {
            Programs = await _programDb.FetchAllByUser(AppState.CurrentUser);
        }

        [RelayCommand]
        private async Task ProgramInfo(Models.Program program)
        {
            await Shell.Current.GoToAsync($"ProgramDetailsPage?workoutId={program.Id}");
        }

        [RelayCommand]
        async void Delete(Models.Program program)
        {
            if(Programs.Contains(program))
            {
                await _programDb.DeleteWorkout(program);
                Programs.Remove(program);
            }
        }
    }
}

