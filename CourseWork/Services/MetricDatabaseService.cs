﻿using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class MetricDatabaseService : BaseDatabaseService, IMetricDatabaseService
	{
        public MetricDatabaseService(SQLiteAsyncConnection database) : base(database)
        { 
        }

        public async Task<ObservableCollection<Metric>> FetchMetrics(User user, DateTime dateFrom, DateTime dateTo)
        {
            try
            
            {
                List<Metric> metrics = await Database.Table<Metric>()
                    .Where(m => m.UserId == user.Id &&m.Date >= dateFrom &&  m.Date <= dateTo).OrderBy(m => m.Date)
                    .ToListAsync();

                ObservableCollection<Metric> filteredMetrics = new ObservableCollection<Metric>(metrics);

                return filteredMetrics;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to fetch metrics: {e.Message}");
            }
        }
        public async Task<int> StoreMetric(Metric metric)
        {
            try
            { 
                var existingMetric = await Database.Table<Metric>().FirstOrDefaultAsync(m => m.Date == metric.Date);
                if (existingMetric == null)
                {
                    return await Database.InsertAsync(metric);
                }
                else
                {
                    existingMetric.Weight = metric.Weight;
                    existingMetric.Height = metric.Height;
                    existingMetric.BodyFat = metric.BodyFat;
                    return await Database.UpdateAsync(existingMetric);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to store metric: {e.Message}");
            }
        }
    }
}

