using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutDatabaseService
	{
		Task<Workout> StoreWorkout(Workout workout,User user);
		Task<List<Workout>> FetchAllByUser(User user);
	}
}

