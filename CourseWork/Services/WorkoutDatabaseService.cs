using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class WorkoutDatabaseService : BaseDatabaseService, IWorkoutDatabaseService
    {
        public async Task<Workout> StoreWorkout(Workout Workout, User User)
        {
            
            try
            {
                Workout.UserId = User.Id;
                await _database.InsertAsync(Workout);
                return await _database.Table<Workout>().Where(m => m.Id == Workout.Id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public async Task<ObservableCollection<Workout>> FetchAllByUser(User User)
        {
            try
            {
                List<Workout> WorkoutsList = await _database.Table<Workout>()
                    .Where(m => m.UserId == User.Id)
                    .ToListAsync();

                ObservableCollection<Workout> Workouts = new ObservableCollection<Workout>(WorkoutsList);

                return Workouts;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.Source);
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public async Task<Workout>FetchById(Guid WorkoutId)
        {
            return await _database.Table<Workout>().Where(m => m.Id == WorkoutId).FirstOrDefaultAsync();
        }

        public async Task<int>DeleteWorkout(Workout Workout)
        {
            return await _database.Table<Workout>().Where(m => m.Id == Workout.Id).DeleteAsync();
        }

        public WorkoutDatabaseService(SQLiteAsyncConnection _database) : base(_database)
		{

		}
	}
}

