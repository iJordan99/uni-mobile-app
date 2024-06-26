﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class ProgramDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {

        [ObservableProperty]
        Guid _workoutId;

        [ObservableProperty]
        Models.WorkoutProgram _workoutProgram;

        [ObservableProperty]
        ObservableCollection<ProgramExercise> _workoutExercises;

        private readonly IWorkoutProgramDatabaseService _workoutProgramDb;

        private readonly IProgramExerciseDatabaseService _programExerciseDb;

        private readonly IWorkoutSessionDatabaseService _workoutSessionDb;

        private readonly IWorkoutSessionExerciseDatabaseService _workoutSessionExerciseDb; 

        public ProgramDetailsPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutProgramDatabaseService workoutProgramDb,
            IProgramExerciseDatabaseService programExerciseDb, IWorkoutSessionDatabaseService workoutSessionDb, IWorkoutSessionExerciseDatabaseService workoutSessionExerciseDb) : base(appState, userDb)
		{
            this._workoutProgramDb = workoutProgramDb;
            this._programExerciseDb = programExerciseDb;
            this._workoutSessionDb = workoutSessionDb;
            this._workoutSessionExerciseDb = workoutSessionExerciseDb;
        }

        //https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            WorkoutId = Guid.Parse((string)query["workoutId"]);
            LoadData();
        }

        private async void LoadData()
        {
            WorkoutProgram = await _workoutProgramDb.FetchById(WorkoutId);
            WorkoutExercises = await _programExerciseDb.FetchWorkoutExercise(WorkoutProgram);
        }

        [RelayCommand]
        private async Task RegisterWorkoutSession()
        {
            var session = new WorkoutSession(AppState.CurrentUser.Id, DateTime.Now);
            session = await _workoutSessionDb.StoreWorkoutSession(session);

            try
            {
                foreach (var exercise in WorkoutExercises)
                {
                    var sessionExercise = new WorkoutSessionExercise(session, exercise.ExerciseName, exercise.Sets,
                        exercise.Reps, exercise.Weight);
                    
                     await _workoutSessionExerciseDb.StoreWorkoutSessionExercise(sessionExercise);
                }
                
                WorkoutExercises.Clear();
                
                if (Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", "Workout Session Saved", "OK");
                }
                
                await Shell.Current.GoToAsync($"WorkoutSessionDetailsPage?sessionId={session.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", e.Message, "OK");
                }
            }
        }
    }
}

