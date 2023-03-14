using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class WorkoutDatabaseService : BaseDatabaseService, IWorkoutDatabaseService
    {
        public async Task<Workout> StoreWorkout(Workout workout, User user)
        {
            
            try
            {
                workout.UserId = user.Id;
                await _database.InsertAsync(workout);
                return await _database.Table<Workout>().Where(m => m.Id == workout.Id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public async Task<List<Workout>> FetchAllByUser(User user)
        {
            try
            {
                List<Workout> workouts = await _database.Table<Workout>()
                    .Where(w => w.UserId == user.Id)
                    .ToListAsync();

                return workouts;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.Source);
                Console.Write(e.StackTrace);
                return null;
            }
        }


        public WorkoutDatabaseService(SQLiteAsyncConnection _database) : base(_database)
		{

		}
	}
}

