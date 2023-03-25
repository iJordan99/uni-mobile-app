using System;
using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class ProgramDatabaseService : BaseDatabaseService, IProgramDatabaseService
    {
        public async Task<Models.Program> StoreProgram(Models.Program program, User user)
        {
            
            try
            {
                program.UserId = user.Id;
                await Database.InsertAsync(program);
                return await Database.Table<Models.Program>().Where(m => m.Id == program.Id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to store program in database: {e.Message}");
            }
        }

        public async Task<ObservableCollection<Models.Program>> FetchAllByUser(User user)
        {
            try
            {
                List<Models.Program> programList = await Database.Table<Models.Program>()
                    .Where(m => m.UserId == user.Id)
                    .ToListAsync();

                ObservableCollection<Models.Program> programs = new ObservableCollection<Models.Program>(programList);

                return programs;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to fetch the user's programs: {e.Message}");
            }
        }

        public async Task<Models.Program>FetchById(Guid programId)
        {
            var program =  await Database.Table<Models.Program>().Where(m => m.Id == programId).FirstOrDefaultAsync();

            if (program != null)
            {
                return program;
            }

            throw new Exception($"Program with id {programId} not found: ");
        }

        public async Task<int>DeleteWorkout(Models.Program program)
        {
            return await Database.Table<Models.Program>().Where(m => m.Id == program.Id).DeleteAsync();
        }

        public ProgramDatabaseService(SQLiteAsyncConnection database) : base(database)
		{

		}
	}
}

