using System;
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

        public async Task<Metric> FetchMetrics(User user, DateTime date)
        {
            try
            {
                Metric metric = await Database.Table<Metric>()
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
        public async Task<int> StoreMetric(Metric metric)
        {
            try
            { 
                return await Database.InsertAsync(metric);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return 0;
            }
        }
    }
}

