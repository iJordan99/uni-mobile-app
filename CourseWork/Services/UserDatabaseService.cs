using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class UserDatabaseService : BaseDatabaseService, IUserDatabaseService
	{
        public UserDatabaseService(SQLiteAsyncConnection database) : base(database)
        {

        }

        public async Task<int> RegisterUser(User user)
        {
            try
            {
                var res = await Database.InsertAsync(user);
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
                var res = await Database.Table<User>().FirstOrDefaultAsync(x =>
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

