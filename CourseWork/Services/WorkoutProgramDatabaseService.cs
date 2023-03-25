using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class WorkoutProgramDatabaseService : BaseDatabaseService, IWorkoutProgramDatabaseService
    {
        public async Task<WorkoutProgram> StoreProgram(WorkoutProgram workoutProgram, User user)
        {
            
            try
            {
                workoutProgram.UserId = user.Id;
                await Database.InsertAsync(workoutProgram);
                return await Database.Table<Models.WorkoutProgram>().Where(m => m.Id == workoutProgram.Id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to store workoutProgram in database: {e.Message}");
            }
        }
        

        public async Task<ObservableCollection<WorkoutProgram>> FetchAllByUser(User user)
        {
            try
            {
                List<Models.WorkoutProgram> programList = await Database.Table<Models.WorkoutProgram>()
                    .Where(m => m.UserId == user.Id)
                    .ToListAsync();

                ObservableCollection<WorkoutProgram> programs = new ObservableCollection<WorkoutProgram>(programList);

                return programs;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to fetch the user's programs: {e.Message}");
            }
        }

        public async Task<WorkoutProgram>FetchById(Guid programId)
        {
            var program =  await Database.Table<Models.WorkoutProgram>().Where(m => m.Id == programId).FirstOrDefaultAsync();

            if (program != null)
            {
                return program;
            }

            throw new Exception($"WorkoutProgram with id {programId} not found: ");
        }

        public async Task<int>DeleteWorkout(WorkoutProgram workoutProgram)
        {
            return await Database.Table<WorkoutProgram>().Where(m => m.Id == workoutProgram.Id).DeleteAsync();
        }

        public WorkoutProgramDatabaseService(SQLiteAsyncConnection database) : base(database)
		{

		}
	}
}

