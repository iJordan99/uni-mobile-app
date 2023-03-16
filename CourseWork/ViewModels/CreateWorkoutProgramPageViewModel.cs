using System.Collections.ObjectModel;
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
		string programName;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
        ProgramExercise exercise;

		[ObservableProperty]
		int sets;

		[ObservableProperty]
		int reps;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
		ObservableCollection<ProgramExercise> exerciseList;

		[ObservableProperty]
        string currentUser;

		[ObservableProperty]
		string exerciseName;

		private readonly IProgramDatabaseService ProgramDb;
		private readonly IProgramExerciseDatabaseService exerciseDB;

        public CreateWorkoutProgramPageViewModel(IAppState appState, IUserDatabaseService userDB, IProgramDatabaseService programDb,
		IProgramExerciseDatabaseService exerciseDB) : base(appState, userDB)
        {
			this.ProgramDb = programDb;
			this.exerciseDB = exerciseDB;
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
			var program = new Models.Program(appState.CurrentUser, ProgramName);

			try
			{
				var res = await ProgramDb.StoreProgram(program, appState.CurrentUser);

				foreach (ProgramExercise exercise in ExerciseList)
				{
					exercise.WorkoutId = res.Id;
					await exerciseDB.StoreWorkoutExercise(exercise);
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
			return !string.IsNullOrEmpty(ProgramName) && ExerciseList.Any(); ;
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

