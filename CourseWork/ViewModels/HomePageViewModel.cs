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
        ObservableCollection<Metric> _metric;

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
        
        public HomePageViewModel(IAppState appState, IUserDatabaseService userDb, IMetricDatabaseService metricDb) : base(appState, userDb)
        {
			this._metricDb = metricDb;
            CurrentUser = appState.CurrentUser.Username;
			WelcomeMessage = $"Welcome back {CurrentUser}";
            Date = DateTime.Today;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetMetric();
        }

        partial void OnDateChanged(DateTime value)
        {
            LoadDataAsync();
        }

        [RelayCommand]
        private async Task GetMetric()
        {
            try
            {
                var dailyMetric = await _metricDb.FetchMetrics(AppState.CurrentUser, Date, Date);
                if (dailyMetric.Any())
                {
                    Weight = dailyMetric[0].Weight;
                    Height = dailyMetric[0].Height;
                    BodyFat = dailyMetric[0].BodyFat;
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        [RelayCommand]
        private async Task SaveMetrics()
        {
            var dailyMetric = new Metric(Weight, Height, BodyFat, Date, AppState.CurrentUser);

            try
            {
                var res = await _metricDb.StoreMetric(dailyMetric);
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

