using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutExerciseDatabaseService
	{
        Task<int> StoreWorkoutExercise(WorkoutExercise workoutExercise);
        Task<WorkoutExercise> FetchWorkoutExercise(Workout workout);
    }
}

