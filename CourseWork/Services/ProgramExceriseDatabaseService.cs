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
            return await _database.InsertAsync(programExercise);
        }

        public async Task<ObservableCollection<ProgramExercise>> FetchWorkoutExercise(Models.Program program)
        {
            try
            {

                List<ProgramExercise> ProgramExercisesList = await _database.Table<ProgramExercise>().Where(m => m.WorkoutId == program.Id)
                            .ToListAsync();

                ObservableCollection<ProgramExercise> ProgramExercises = new ObservableCollection<ProgramExercise>(ProgramExercisesList);

                return ProgramExercises;

            } catch(Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public ProgramExceriseDatabaseService(SQLiteAsyncConnection _database) : base(_database)
		{

		}
	}
}

