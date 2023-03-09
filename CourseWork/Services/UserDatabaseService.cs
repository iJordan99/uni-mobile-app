using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class UserDatabaseService : IUserDatabaseService
	{
        private readonly SQLiteAsyncConnection _database;

        public UserDatabaseService()
		{
            //create new db connection 
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            _database.CreateTableAsync<User>();
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
                return null;
            }
        }
    }
}

