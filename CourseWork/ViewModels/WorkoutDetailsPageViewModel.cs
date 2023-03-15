using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;
using Microsoft.Maui.Controls;

namespace CourseWork.ViewModels
{
	public partial class WorkoutDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {

        [ObservableProperty]
        Guid workoutId;

        [ObservableProperty]
        Workout workout;

        [ObservableProperty]
        ObservableCollection<WorkoutExercise> workoutExercises;

        protected readonly IWorkoutDatabaseService workoutDB;
        protected readonly IWorkoutExerciseDatabaseService exerciseDB;

        public WorkoutDetailsPageViewModel(IAppState appState, IUserDatabaseService userDB, IWorkoutDatabaseService workoutDB,
            IWorkoutExerciseDatabaseService exerciseDB) : base(appState, userDB)
		{
            this.workoutDB = workoutDB;
            this.exerciseDB = exerciseDB;
        }

        //https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkoutId = Guid.Parse((string)query["workoutId"]);
            LoadData();
        }

        private async void LoadData()
        {
            Workout = await workoutDB.FetchById(WorkoutId);
            WorkoutExercises = await exerciseDB.FetchWorkoutExercise(Workout);
        }
    }
}

