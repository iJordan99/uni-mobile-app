using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class MetricDatabaseService : BaseDatabaseService, IMetricDatabaseService
	{
        public MetricDatabaseService(SQLiteAsyncConnection _database) : base(_database)
        { 
        }

        public async Task<Metric> FetchMetrics(User user, DateTime date)
        {
            try
            {
                Metric metric = await _database.Table<Metric>()
                                .Where(m => m.UserId == user.Id && m.Date == date)
                                .FirstOrDefaultAsync();
                return metric;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.Source);
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public async Task<int> StoreMetric(Metric metric, User user)
        {
            try
            {

                Metric existingMetric = await FetchMetrics(user, metric.Date);
                if (existingMetric != null)
                {
                    existingMetric.Weight = metric.Weight;
                    existingMetric.Height = metric.Height;
                    existingMetric.BodyFat = metric.BodyFat;

                    return await _database.UpdateAsync(existingMetric);
                }
                else
                {
                    metric.UserId = user.Id;
                    return await _database.InsertAsync(metric);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return 0;
            }
        }
    }
}

