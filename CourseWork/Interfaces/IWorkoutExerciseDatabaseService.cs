using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutExerciseDatabaseService
	{
        Task<int> StoreWorkoutExercise(WorkoutExercise WorkoutExercise);
        Task<ObservableCollection<WorkoutExercise>> FetchWorkoutExercise(Workout Workout);
    }
}

