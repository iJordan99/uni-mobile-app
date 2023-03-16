using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutSessionDatabaseService
	{
		Task<WorkoutSession> StoreWorkoutSession(WorkoutSession workoutSession);
		Task<ObservableCollection<WorkoutSession>>FetchSessions(User user);
	}
}

