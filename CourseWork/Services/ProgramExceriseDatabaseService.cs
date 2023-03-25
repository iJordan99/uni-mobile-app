using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class ProgramExceriseDatabaseService : BaseDatabaseService, IProgramExerciseDatabaseService
	{

        public async Task<int> StoreWorkoutExercise(ProgramExercise programExercise)
        {
            try
            {
               return await Database.InsertAsync(programExercise);
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to store workout exercise: {e.Message}");
            }
        }

        public async Task<ObservableCollection<ProgramExercise>> FetchWorkoutExercise(Models.Program program)
        {
            try
            {

                List<ProgramExercise> programExercisesList = await Database.Table<ProgramExercise>().Where(m => m.WorkoutId == program.Id)
                            .ToListAsync();

                ObservableCollection<ProgramExercise> programExercises = new ObservableCollection<ProgramExercise>(programExercisesList);

                return programExercises;

            } catch(Exception e)
            {
                throw new Exception($"Unable to fetch workout exercise: {e.Message}");
            }
        }

        public ProgramExceriseDatabaseService(SQLiteAsyncConnection database) : base(database)
		{

		}
	}
}

