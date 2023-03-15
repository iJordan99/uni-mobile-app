using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutDatabaseService
	{
		Task<Workout> StoreWorkout(Workout Workout,User User);
		Task<ObservableCollection<Workout>> FetchAllByUser(User User);
		Task<Workout> FetchById(Guid WorkoutId);
		Task<int> DeleteWorkout(Workout Workout);
	}
}

