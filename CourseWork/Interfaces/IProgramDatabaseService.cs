using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IProgramDatabaseService
	{
		Task<Models.Program> StoreProgram(Models.Program program,User user);
		Task<ObservableCollection<Models.Program>> FetchAllByUser(User user);
		Task<Models.Program> FetchById(Guid programId);
		Task<int> DeleteWorkout(Models.Program program);
	}
}

