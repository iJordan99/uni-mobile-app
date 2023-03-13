using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public partial class HomePageViewModel : BaseViewModel
	{
		[ObservableProperty]
		string currentUser;

        [ObservableProperty]
        string welcomeMessage;

		[ObservableProperty]
		double weight;

        [ObservableProperty]
        double height;

        [ObservableProperty]
        double bodyFat;

		[ObservableProperty]
		Metric metric;

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        bool canUpdate;

        [ObservableProperty]
        bool hasNoMetric;

        protected readonly IMetricDatabaseService metricDB;

        public HomePageViewModel(IAppState appState, IUserDatabaseService userDB, IMetricDatabaseService metricDB) : base(appState, userDB)
        {
			this.metricDB = metricDB;
            CurrentUser = appState.CurrentUser.Username;
			WelcomeMessage = $"Welcome back {CurrentUser}";
            Date = DateTime.Today;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetMetric();
        }

        [RelayCommand]
        private async Task GetMetric()
        {
            Metric = await metricDB.FetchMetrics(appState.CurrentUser, Date);

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
            Metric = new Metric()
            {
                Weight = Weight,
                Height = Height,
                BodyFat = BodyFat,
                Date = Date
            };

            try
            {
                var res = await metricDB.StoreMetric(Metric, appState.CurrentUser);
                if(res != 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", "Metrics Saved", "OK");
                    HasNoMetric = false;
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Oh no!", $"Encountered {e.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToProgressPage()
        {
            await Shell.Current.GoToAsync("/ProgressPage");
        }
    }
}

