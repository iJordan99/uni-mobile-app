using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IMetricDatabaseService
	{
		Task<ObservableCollection<Metric>>FetchMetrics(User user, DateTime dateFrom, DateTime dateTo);
		Task<int>StoreMetric(Metric metric);
	}
}

