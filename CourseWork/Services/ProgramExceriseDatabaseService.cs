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
            return await Database.InsertAsync(programExercise);
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
                Console.Write(e.Message);
                return null;
            }
        }

        public ProgramExceriseDatabaseService(SQLiteAsyncConnection database) : base(database)
		{

		}
	}
}

