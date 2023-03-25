using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IWorkoutProgramDatabaseService
	{
		Task<WorkoutProgram> StoreProgram(WorkoutProgram workoutProgram,User user);
		Task<ObservableCollection<WorkoutProgram>> FetchAllByUser(User user);
		Task<WorkoutProgram> FetchById(Guid programId);
		Task<int> DeleteWorkout(WorkoutProgram workoutProgram);
	}
}

