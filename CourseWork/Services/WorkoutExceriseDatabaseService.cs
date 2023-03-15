using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class WorkoutExceriseDatabaseService : BaseDatabaseService, IWorkoutExerciseDatabaseService
	{

        public async Task<int> StoreWorkoutExercise(WorkoutExercise WorkoutExercise)
        {
            return await _database.InsertAsync(WorkoutExercise);
        }

        public async Task<ObservableCollection<WorkoutExercise>> FetchWorkoutExercise(Workout Workout)
        {
            try
            {

                List<WorkoutExercise> WorkoutExercisesList = await _database.Table<WorkoutExercise>().Where(m => m.WorkoutId == Workout.Id)
                            .ToListAsync();

                ObservableCollection<WorkoutExercise> WorkoutExercises = new ObservableCollection<WorkoutExercise>(WorkoutExercisesList);

                return WorkoutExercises;

            } catch(Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public WorkoutExceriseDatabaseService(SQLiteAsyncConnection _database) : base(_database)
		{

		}
	}
}

