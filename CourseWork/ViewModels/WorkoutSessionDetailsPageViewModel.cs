using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels;

public partial class WorkoutSessionDetailsPageViewModel : BaseViewModel, IQueryAttributable
{
    
    [ObservableProperty]
    Guid _sessionId;

    [ObservableProperty]
    WorkoutSession _session;

    [ObservableProperty] 
    ObservableCollection<WorkoutSessionExercise> _sessionExercises;
    
    private readonly IWorkoutSessionDatabaseService _workoutSessionDb;

    private readonly IWorkoutSessionExerciseDatabaseService _workoutSessionExerciseDb; 
    
    public WorkoutSessionDetailsPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutSessionExerciseDatabaseService workoutSessionExerciseDb, IWorkoutSessionDatabaseService workoutSessionDb) : base(appState, userDb)
    {
        this._workoutSessionExerciseDb = workoutSessionExerciseDb;
        this._workoutSessionDb = workoutSessionDb;
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SessionId = Guid.Parse((string)query["sessionId"]);
        LoadData();
    }
    
    private async void LoadData()
    {
        Session = await _workoutSessionDb.FetchById(SessionId);
        SessionExercises = await _workoutSessionExerciseDb.FetchSessionExercises(Session);
    }
}