using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class WorkoutSessionsPageViewModel : BaseViewModel
	{
		
		[ObservableProperty]
		ObservableCollection<WorkoutSession> _sessions;
		
		[ObservableProperty] 
		bool _isRefreshing;
		
		private readonly IWorkoutSessionDatabaseService _workoutSessionDb;
		
		public WorkoutSessionsPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutSessionDatabaseService workoutSessionDb) : base(appState, userDb)
		{
			this._workoutSessionDb = workoutSessionDb;
			GetSessions();
		}

		[RelayCommand]
		private async Task GetSessions()
		{
			//https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/refreshview?view=net-maui-7.0
			IsRefreshing = true;
			Sessions = await _workoutSessionDb.FetchSessions(AppState.CurrentUser);
			IsRefreshing = false;
		}
		
		[RelayCommand]
		private async Task SessionInfo(Models.WorkoutSession workoutSession)
		{	
			await Shell.Current.GoToAsync($"WorkoutSessionDetailsPage?sessionId={workoutSession.Id}");
		}
	}
}

