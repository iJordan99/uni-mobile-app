using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IProgramExerciseDatabaseService
	{
        Task<int> StoreWorkoutExercise(ProgramExercise programExercise);
        Task<ObservableCollection<ProgramExercise>> FetchWorkoutExercise(WorkoutProgram workoutProgram);
    }
}

