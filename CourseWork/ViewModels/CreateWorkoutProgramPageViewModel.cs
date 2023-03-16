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
		string _programName;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(CreateWorkoutCommand))]
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

		private readonly IProgramDatabaseService _programDb;
		private readonly IProgramExerciseDatabaseService _programExerciseDb;

        public CreateWorkoutProgramPageViewModel(IAppState appState, IUserDatabaseService userDb, IProgramDatabaseService programDb,
		IProgramExerciseDatabaseService programExerciseDb) : base(appState, userDb)
        {
			this._programDb = programDb;
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
			var program = new Models.Program(AppState.CurrentUser, ProgramName);

			try
			{
				var res = await _programDb.StoreProgram(program, AppState.CurrentUser);

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
			return !string.IsNullOrEmpty(ProgramName) && ExerciseList.Any(); ;
		}

		private void ResetFields()
		{
			ExerciseList.Clear();
			ProgramName = string.Empty;
            ExerciseName = "";
			Sets = 0;
            Reps = 0;
        }
	}
}

