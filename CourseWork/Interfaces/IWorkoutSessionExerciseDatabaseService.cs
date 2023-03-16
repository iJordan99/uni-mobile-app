using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutSessionExerciseDatabaseService
	{
		Task<int> StoreWorkoutSessionExercise(WorkoutSessionExercise workoutSessionExercise);
		Task<ObservableCollection<WorkoutSessionExercise>>FetchSessionExercises(WorkoutSession workoutSession);
	}
}

