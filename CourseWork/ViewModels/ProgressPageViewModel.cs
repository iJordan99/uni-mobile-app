using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;
using CourseWork.Models;

using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace CourseWork.ViewModels
{
    public partial class ProgressPageViewModel : BaseViewModel
    {
        private readonly IMetricDatabaseService _metricDb;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetUserProgressMetricsCommand))]
        DateTime _dateFrom;

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
        bool _hasMetric;

        public ProgressPageViewModel(IAppState appState, IUserDatabaseService userDb, IMetricDatabaseService metricDb) : base(appState, userDb)
        {
            this._metricDb = metricDb;
            HasMetric = false;
        }

        partial void OnDateFromChanged(DateTime value)
        {
            GetUserProgressMetrics();
        }
        
        [ObservableProperty]
        List<double> _chartValues;
        
        [ObservableProperty] ISeries[] _series;
        
        public LabelVisual Title { get; set; } =
            new LabelVisual
            {
                Text = "Weight Loss Journey",
                TextSize = 40,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

        [RelayCommand]
        private async Task GetUserProgressMetrics()
        {
            var metrics = await _metricDb.FetchMetrics(AppState.CurrentUser, DateFrom, DateTime.Now);
            if (metrics.Any())
            {
                HasMetric = true;
                var progress = GetMetricDifference(metrics);
                CalculateProgress(progress["Weight"],progress["BodyFat"]);
                ChartValues = ChartMetrics(metrics);
                
                Series = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        Values = ChartValues,
                        Fill = null
                    }
                };  
            }
            else
            {
                if (Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Dates", "No Metrics Found", "OK");
                }
                HasMetric = false;
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

        private List<double> ChartMetrics(ObservableCollection<Metric> metrics)
        {
            var chartValues = metrics.Select(metric => metric.Weight).ToList();
            return chartValues;
        }

        
    }
}

