using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IMetricDatabaseService
	{
		Task<Metric> FetchMetrics(User user, DateTime date);
		Task<int> StoreMetric(Metric metric);
	}
}

