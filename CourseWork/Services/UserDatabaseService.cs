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
                return await Database.InsertAsync(user);
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to register user: {e.Message}");
            }

        }

        public async Task<User> ValidateUser(User user)
        {

            try
            {
                return await Database.Table<User>().FirstOrDefaultAsync(x =>
                    x.Username == user.Username && x.Password == user.Password);
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to validate user: {e.Message}");
            }
        }
    }
}