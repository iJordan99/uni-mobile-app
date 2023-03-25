using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IUserDatabaseService
	{
		Task<int> RegisterUser(User user);
		Task<User> ValidateUser(User user);
		Task<bool> CheckIfUser(User user);
	}
}

