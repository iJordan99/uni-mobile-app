using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class HomePageViewModel : BaseViewModel
	{
		[ObservableProperty]
		string _currentUser;

        [ObservableProperty]
        string _welcomeMessage;

		[ObservableProperty]
		double _weight;

        [ObservableProperty]
        double _height;

        [ObservableProperty]
        double _bodyFat;

		[ObservableProperty]
		Metric _metric;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetMetricCommand))]
        DateTime _date;

        [ObservableProperty]
        bool _canUpdate;

        [ObservableProperty]
        bool _hasNoMetric;

        [ObservableProperty] ObservableCollection<WorkoutSession> _workoutSessions;
        
        [ObservableProperty] ObservableCollection<WorkoutSessionExercise> _sessionExercises;

        private readonly IMetricDatabaseService _metricDb;

        private readonly IWorkoutSessionDatabaseService _workoutSessionDb;

        private readonly IWorkoutSessionExerciseDatabaseService _workoutSessionExerciseDb;

        public HomePageViewModel(IAppState appState, IUserDatabaseService userDb, IMetricDatabaseService metricDb, IWorkoutSessionDatabaseService workoutSessionDb, IWorkoutSessionExerciseDatabaseService workoutSessionExerciseDb) : base(appState, userDb)
        {
			this._metricDb = metricDb;
            this._workoutSessionDb = workoutSessionDb;
            this._workoutSessionExerciseDb = workoutSessionExerciseDb;
            CurrentUser = appState.CurrentUser.Username;
			WelcomeMessage = $"Welcome back {CurrentUser}";
            Date = DateTime.Today;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetMetric();
            await GetWorkoutSessionsInfo();
        }

        partial void OnDateChanged(DateTime value)
        {
            LoadDataAsync();
        }

        private async Task GetWorkoutSessionsInfo()
        {
            WorkoutSessions = await _workoutSessionDb.FetchSessions(AppState.CurrentUser);
            
            foreach (var session in WorkoutSessions)
            {
                SessionExercises = await _workoutSessionExerciseDb.FetchSessionExercises(session);
            }
        }
        
        [RelayCommand]
        private async Task GetMetric()
        {
            Metric = await _metricDb.FetchMetrics(AppState.CurrentUser, Date);

            if (Metric != null)
            {
                Weight = Metric.Weight;
                Height= Metric.Height;
                BodyFat = Metric.BodyFat;
                HasNoMetric = false;
            }
            else
            {
                Weight = 0;
                Height = 0;
                BodyFat = 0;
                HasNoMetric = true;
            }
        }


        [RelayCommand]
        private async Task SaveMetrics()
        {
            Metric = new Metric(Weight, Height, BodyFat, Date, AppState.CurrentUser);

            try
            {
                var res = await _metricDb.StoreMetric(Metric);
                if(res != 0)
                {
                    if (Application.Current.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", "Metrics Saved", "OK");
                    }
                    
                    HasNoMetric = false;
                }
            }
            catch (Exception e)
            {
                if (Application.Current != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Oh no!", $"Encountered {e.Message}", "OK");
                }
            }
        }

        [RelayCommand]
        private async Task NavigateToProgressPage()
        {
            await Shell.Current.GoToAsync("/ProgressPage");
        }
    }
}

