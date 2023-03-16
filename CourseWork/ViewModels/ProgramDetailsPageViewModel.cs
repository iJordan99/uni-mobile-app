using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class ProgramDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {

        [ObservableProperty]
        Guid workoutId;

        [ObservableProperty]
        Models.Program _program;

        [ObservableProperty]
        double weight;

        [ObservableProperty]
        ObservableCollection<ProgramExercise> workoutExercises;

        private readonly IProgramDatabaseService ProgramDb;

        private readonly IProgramExerciseDatabaseService exerciseDB;
        //protected readonly IRegisterWorkoutSession workoutSessionDB;

        public ProgramDetailsPageViewModel(IAppState appState, IUserDatabaseService userDB, IProgramDatabaseService programDb,
            IProgramExerciseDatabaseService exerciseDB) : base(appState, userDB)
		{
            this.ProgramDb = programDb;
            this.exerciseDB = exerciseDB;
            //this.workoutSessionDB = workoutSessionDB;
        }

        //https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkoutId = Guid.Parse((string)query["workoutId"]);
            LoadData();
        }

        private async void LoadData()
        {
            Program = await ProgramDb.FetchById(WorkoutId);
            WorkoutExercises = await exerciseDB.FetchWorkoutExercise(Program);
        }

        //[RelayCommand]
        //private async Task RegisterWorkoutSession()
        //{

        //    try
        //    {
                
        //        Program NewWorkout = new Program(appState.CurrentUser, Program.ProgramName);
        //        NewWorkout = await programDb.StoreProgram(NewWorkout, appState.CurrentUser);

        //        foreach (ProgramExercise exercise in WorkoutExercises)
        //        {
        //            exercise.WorkoutId = NewWorkout.Id;
        //            await exerciseDB.StoreWorkoutExercise(exercise);
        //        }

        //        var i = await workoutSessionDB.StoreWorkoutSession(NewWorkout, appState.CurrentUser);
        //        await Application.Current.MainPage.DisplayAlert("Oh no!", $"{i}", "OK");
        //    }
        //    catch (Exception e)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Oh no!", $"Encountered {e.Message}", "OK");
        //    }
        //}
    }
}

