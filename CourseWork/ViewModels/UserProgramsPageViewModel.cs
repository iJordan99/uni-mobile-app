using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
    public partial class UserProgramsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Models.WorkoutProgram> _programs;

        [ObservableProperty] 
        bool _isRefreshing;

        private readonly IWorkoutProgramDatabaseService _workoutProgramDb;
       
        public UserProgramsPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutProgramDatabaseService workoutProgramDb) : base(appState, userDb)
        {
            this._workoutProgramDb = workoutProgramDb;
            Programs = new ObservableCollection<Models.WorkoutProgram>();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetPrograms();
        }

        private async Task GetPrograms()
        {
            //https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/refreshview?view=net-maui-7.0
            IsRefreshing = true;
            Programs = await _workoutProgramDb.FetchAllByUser(AppState.CurrentUser);
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task RefreshData()
        {
            await GetPrograms();
        }

        [RelayCommand]
        private async Task ProgramInfo(Models.WorkoutProgram workoutProgram)
        {
            await Shell.Current.GoToAsync($"ProgramDetailsPage?workoutId={workoutProgram.Id}");
        }

        [RelayCommand]
        async void Delete(Models.WorkoutProgram workoutProgram)
        {
            if(Programs.Contains(workoutProgram))
            {
                await _workoutProgramDb.DeleteWorkout(workoutProgram);
                Programs.Remove(workoutProgram);
            }
        }
    }
}

