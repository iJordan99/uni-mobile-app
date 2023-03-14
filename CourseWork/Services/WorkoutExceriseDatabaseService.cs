using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class WorkoutExceriseDatabaseService : BaseDatabaseService, IWorkoutExerciseDatabaseService
	{

        public async Task<int> StoreWorkoutExercise(WorkoutExercise workoutExercise)
        {
            return await _database.InsertAsync(workoutExercise);
        }

        public async Task<WorkoutExercise> FetchWorkoutExercise(Workout workout)
        {
            try
            {

                return await _database.Table<WorkoutExercise>()
                                .Where(m => m.WorkoutId == workout.Id)
                                .FirstOrDefaultAsync();

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

