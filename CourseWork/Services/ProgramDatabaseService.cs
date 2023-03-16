using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class ProgramDatabaseService : BaseDatabaseService, IProgramDatabaseService
    {
        public async Task<Models.Program> StoreProgram(Models.Program program, User User)
        {
            
            try
            {
                program.UserId = User.Id;
                await _database.InsertAsync(program);
                return await _database.Table<Models.Program>().Where(m => m.Id == program.Id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        public async Task<ObservableCollection<Models.Program>> FetchAllByUser(User User)
        {
            try
            {
                List<Models.Program> ProgramList = await _database.Table<Models.Program>()
                    .Where(m => m.UserId == User.Id)
                    .ToListAsync();

                ObservableCollection<Models.Program> Programs = new ObservableCollection<Models.Program>(ProgramList);

                return Programs;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.Source);
                Console.Write(e.StackTrace);
                return null;
            }
        }

        public async Task<Models.Program>FetchById(Guid ProgramId)
        {
            return await _database.Table<Models.Program>().Where(m => m.Id == ProgramId).FirstOrDefaultAsync();
        }

        public async Task<int>DeleteWorkout(Models.Program program)
        {
            return await _database.Table<Models.Program>().Where(m => m.Id == program.Id).DeleteAsync();
        }

        public ProgramDatabaseService(SQLiteAsyncConnection _database) : base(_database)
		{

		}
	}
}

