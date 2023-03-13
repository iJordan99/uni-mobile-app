using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class UserDatabaseService : BaseDatabaseService, IUserDatabaseService
	{
        public UserDatabaseService(SQLiteAsyncConnection _database) : base(_database)
        {

        }

        public async Task<int> RegisterUser(User user)
        {
            try
            {
                var res = await _database.InsertAsync(user);
                return res;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return 0;
            }

        }

        public async Task<User> ValidateUser(User user)
        {

            try
            {
                var res = await _database.Table<User>().FirstOrDefaultAsync(x =>
                                            x.Username == user.Username && x.Password == user.Password);
                return res;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return null;
            }
        }
    }
}

