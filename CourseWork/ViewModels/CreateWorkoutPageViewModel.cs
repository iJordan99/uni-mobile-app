using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public partial class CreateWorkoutPageViewModel : BaseViewModel
	{
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
		string workoutName;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
        WorkoutExercise exercise;

		[ObservableProperty]
		int sets;

		[ObservableProperty]
		int reps;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
		ObservableCollection<WorkoutExercise> exerciseList;

		[ObservableProperty]
        string currentUser;

		[ObservableProperty]
		string exerciseName;

        protected readonly IWorkoutDatabaseService workoutDB;
		protected readonly IWorkoutExerciseDatabaseService exerciseDB;

        public CreateWorkoutPageViewModel(IAppState appState, IUserDatabaseService userDB, IWorkoutDatabaseService workoutDB,
		IWorkoutExerciseDatabaseService exerciseDB) : base(appState, userDB)
        {
			this.workoutDB = workoutDB;
			this.exerciseDB = exerciseDB;
            ExerciseList = new ObservableCollection<WorkoutExercise>();
            CurrentUser = appState.CurrentUser.Username;
        }

        [RelayCommand]
		void Add()
		{
			if(!string.IsNullOrEmpty(ExerciseName) && Sets != 0 && Reps != 0)
			{
                WorkoutExercise exercise = new WorkoutExercise()
				{
					ExerciseName = ExerciseName,
					Sets = Sets,
					Reps = Reps,
					Weight = 0
				};
				ExerciseList.Add(exercise);
			}
			ExerciseName = "";
			Sets = 0;
			Reps = 0;
		}

		[RelayCommand]
		void Delete(WorkoutExercise excersise)
		{
			if (ExerciseList.Contains(excersise))
			{
				ExerciseList.Remove(excersise);
			}
		}

		[RelayCommand(CanExecute = nameof(CanCreate))]
		private async Task CreateWorkout()
		{
			Workout workout = new Workout(appState.CurrentUser, WorkoutName);

			try
			{
				var res = await workoutDB.StoreWorkout(workout, appState.CurrentUser);

				foreach (WorkoutExercise exercise in ExerciseList)
				{
					exercise.WorkoutId = res.Id;
					await exerciseDB.StoreWorkoutExercise(exercise);
				}

			}
			catch (Exception e)
			{
				Console.Write(e.Message);
			}
		}

		private bool CanCreate()
		{
			return !string.IsNullOrEmpty(WorkoutName) && ExerciseList.Any(); ;
		}


		[RelayCommand]
		private async Task Fetch()
		{

			var t = await workoutDB.FetchAllByUser(appState.CurrentUser);
			var e = await exerciseDB.FetchWorkoutExercise(t.First());
		}

	}
}

