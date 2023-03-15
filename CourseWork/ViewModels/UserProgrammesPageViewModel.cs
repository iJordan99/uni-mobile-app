using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public partial class UserProgrammesPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Workout> programmes;

        protected readonly IWorkoutDatabaseService workoutDB;
       
        public UserProgrammesPageViewModel(IAppState appState, IUserDatabaseService userDB, IWorkoutDatabaseService workoutDB) : base(appState, userDB)
        {
            this.workoutDB = workoutDB;
            Programmes = new ObservableCollection<Workout>();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetProgrammes();
        }

        private async Task GetProgrammes()
        {
            Programmes = await workoutDB.FetchAllByUser(appState.CurrentUser);
        }

        [RelayCommand]
        private async Task ProgrammeInfo(Workout Workout)
        {
            await Shell.Current.GoToAsync($"/WorkoutDetailsPage?workoutId={Workout.Id}");
        }

        [RelayCommand]
        async void Delete(Workout Workout)
        {
            if(Programmes.Contains(Workout))
            {
                await workoutDB.DeleteWorkout(Workout);
                Programmes.Remove(Workout);
            }
        }
    }
}

