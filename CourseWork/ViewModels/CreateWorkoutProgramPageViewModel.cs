﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public partial class CreateWorkoutProgramPageViewModel : BaseViewModel
	{
		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
		string _programName;
		
		[ObservableProperty]
		ProgramExercise _exercise;

		[ObservableProperty]
		int _sets;

		[ObservableProperty]
		int _reps;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
		ObservableCollection<ProgramExercise> _exerciseList;

		[ObservableProperty]
        string _currentUser;

		[ObservableProperty]
		string _exerciseName;

		private readonly IWorkoutProgramDatabaseService _workoutProgramDb;
		private readonly IProgramExerciseDatabaseService _programExerciseDb;

        public CreateWorkoutProgramPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutProgramDatabaseService workoutProgramDb,
		IProgramExerciseDatabaseService programExerciseDb) : base(appState, userDb)
        {
			this._workoutProgramDb = workoutProgramDb;
			this._programExerciseDb = programExerciseDb;
            ExerciseList = new ObservableCollection<ProgramExercise>();
            CurrentUser = appState.CurrentUser.Username;
        }

        [RelayCommand]
        private void Add()
		{
			if(!string.IsNullOrEmpty(ExerciseName) && Sets != 0 && Reps != 0)
			{
                var exercise = new ProgramExercise()
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
		private void Delete(ProgramExercise exercise)
		{
			if (ExerciseList.Contains(exercise))
			{
				ExerciseList.Remove(exercise);
			}
		}

		[RelayCommand(CanExecute = nameof(CanCreate))]
		private async Task CreateWorkout()
		{
			var program = new WorkoutProgram(AppState.CurrentUser, ProgramName);

			try
			{
				var res = await _workoutProgramDb.StoreProgram(program, AppState.CurrentUser);

				foreach (ProgramExercise exercise in ExerciseList)
				{
					exercise.WorkoutId = res.Id;
					await _programExerciseDb.StoreWorkoutExercise(exercise);
				}

                await Application.Current.MainPage.DisplayAlert("Success!", "Programme Saved", "OK");
                ResetFields();

			}
			catch (Exception e)
			{
                await Application.Current.MainPage.DisplayAlert("Oh no!", $"Encountered {e.Message}", "OK");
            }
		}


		private bool CanCreate()
		{
			return !string.IsNullOrEmpty(ProgramName) && ExerciseList.Any();
		}

		private void ResetFields()
		{
			ExerciseList.Clear();
			ProgramName = "";
			ExerciseName = "";
			Sets = 0;
			Reps = 0;
		}
	}
}

