using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public partial class ProgressPageViewModel : BaseViewModel
    {
        private readonly IMetricDatabaseService metricDB;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CompareMetricsCommand))]
        DateTime dateFrom;

        [ObservableProperty]
        DateTime dateTo;

        [ObservableProperty]
        Metric dateFromMetric;

        [ObservableProperty]
        Metric dateToMetric;

        [ObservableProperty]
        double weightDiff;

        [ObservableProperty]
        double bodyFatDiff;

        [ObservableProperty]
        bool hasLost;

        [ObservableProperty]
        bool hasGained;

        [ObservableProperty]
        string nullMetric;

        [ObservableProperty]
        bool hasNoMetric;

        public ProgressPageViewModel(IAppState appState, IUserDatabaseService userDB, IMetricDatabaseService metricDB) : base(appState, userDB)
        {
            this.metricDB = metricDB;
            DateTo = DateTime.Today;
            DateFrom = DateTime.Today;
        }

        [RelayCommand]
        private async Task CompareMetrics()
        {
            DateFromMetric = await metricDB.FetchMetrics(appState.CurrentUser, DateFrom);
            DateToMetric = await metricDB.FetchMetrics(appState.CurrentUser, DateTo);

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

