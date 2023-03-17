using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public partial class ProgressPageViewModel : BaseViewModel
    {
        private readonly IMetricDatabaseService _metricDb;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetUserProgressMetricsCommand))]
        DateTime _dateFrom;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetUserProgressMetricsCommand))]
        DateTime _dateTo;

        [ObservableProperty]
        Metric _dateFromMetric;

        [ObservableProperty]
        Metric _dateToMetric;

        [ObservableProperty]
        double _weightDiff;

        [ObservableProperty]
        double _bodyFatDiff;

        [ObservableProperty]
        bool _hasLost;

        [ObservableProperty]
        bool _hasGained;

        [ObservableProperty]
        string _nullMetric;

        [ObservableProperty]
        bool _hasNoMetric;

        public ProgressPageViewModel(IAppState appState, IUserDatabaseService userDb, IMetricDatabaseService metricDb) : base(appState, userDb)
        {
            this._metricDb = metricDb;
            DateTo = DateTime.Today;
            DateFrom = DateTime.Today;
            LoadDataAsync();
        }

        partial void OnDateFromChanged(DateTime value)
        {
            GetUserProgressMetrics();
        }

        partial void OnDateToChanged(DateTime value)
        {
            GetUserProgressMetrics();
        }
        
        private async Task LoadDataAsync()
        {
            await GetUserProgressMetrics();
        }

        [RelayCommand]
        private async Task GetUserProgressMetrics()
        {
            var metrics = await _metricDb.FetchMetrics(AppState.CurrentUser, DateFrom, DateTo);
            if (metrics.Any())
            {
                var progress = GetMetricDifference(metrics);
                CalculateProgress(progress["Weight"],progress["BodyFat"]);
            }
            else
            {
                NullMetric = $"No metrics found for {DateOnly.FromDateTime(DateFrom)} & {DateOnly.FromDateTime(DateTo)}";
                HasNoMetric = true;
            }
        }

        private static Dictionary<string, double> GetMetricDifference(ObservableCollection<Metric> metrics)
        {
            double totalWeightDiff = 0;
            double totalBodyFatDiff = 0;

            //need to check if total metrics = 2 as the for loop wont account for the 2nd metric
            if (metrics.Count == 2)
            {
                totalWeightDiff = metrics[1].Weight - metrics[0].Weight;
                totalBodyFatDiff = metrics[1].BodyFat - metrics[0].BodyFat;
            }
            else
            {
                for (var i = 1; i < metrics.Count; i++)
                {
                    var weightDifference = metrics[i].Weight - metrics[i - 1].Weight;
                    var bodyFatDifference = metrics[i].BodyFat - metrics[i - 1].BodyFat;

                    totalWeightDiff = totalWeightDiff + weightDifference;
                    totalBodyFatDiff = totalBodyFatDiff + bodyFatDifference;
                }
            }
            
            var metricDiffDict = new Dictionary<string, double>
            {
                { "Weight", totalWeightDiff },
                { "BodyFat", totalBodyFatDiff }
            };

            return metricDiffDict;
        }
        
        private void CalculateProgress(double progressWeight, double progressBodyFat)
        {
            if (progressWeight < 0)
            {
                HasLost = true;
                HasGained = false;
                WeightDiff = Math.Round(progressWeight - (progressWeight * 2),2);
            }
            else
            {
                HasLost = false;
                HasGained = true;
                WeightDiff = Math.Round(progressWeight,2);
            }
            
            if (progressBodyFat < 0)
            {
                BodyFatDiff =  Math.Round(progressBodyFat - (progressBodyFat * 2),2);
            }
            else
            {
                BodyFatDiff = Math.Round(progressBodyFat, 2);
            }
        }
    }
}

