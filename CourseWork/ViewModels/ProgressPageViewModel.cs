using System;
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
        [NotifyCanExecuteChangedFor(nameof(CompareMetricsCommand))]
        DateTime _dateFrom;

        [ObservableProperty]
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
        }

        [RelayCommand]
        private async Task CompareMetrics()
        {
            DateFromMetric = await _metricDb.FetchMetrics(AppState.CurrentUser, DateFrom);
            DateToMetric = await _metricDb.FetchMetrics(AppState.CurrentUser, DateTo);

            NullMetric = "";

            if (DateToMetric != null && DateFromMetric != null)
            {
                var progress = GetMetricDifference(DateToMetric, DateFromMetric);


                WeightDiff = progress.Weight;
                HasGained = true;
                HasLost = false;
                if (progress.Weight < 0)
                {
                    WeightDiff = progress.Weight - (progress.Weight * 2);
                    HasLost = true;
                    HasGained = false;
                }

                BodyFatDiff = progress.BodyFat;
                if (progress.BodyFat < 0)
                {
                    BodyFatDiff = progress.BodyFat - (progress.BodyFat * 2);
                    return;
                }
            }
            else
            {
                if (DateToMetric == null)
                {
                    NullMetric = $"No metrics found for {DateOnly.FromDateTime(DateTo)}";
                    return;
                }

                NullMetric = $"No metrics found for {DateOnly.FromDateTime(DateFrom)}";
                HasNoMetric = true;
            }
        }

        private static Metric GetMetricDifference(Metric metric1, Metric metric2)
        {
            var res = new Metric
            {
                Weight = Math.Round(metric1.Weight - metric2.Weight,2),
                BodyFat = Math.Round(metric1.BodyFat - metric2.BodyFat,2)
            };

            return res;
        }
    }
}

