using System;
using System.Collections.ObjectModel;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IProgramDatabaseService
	{
		Task<Models.Program> StoreProgram(Models.Program program,User User);
		Task<ObservableCollection<Models.Program>> FetchAllByUser(User User);
		Task<Models.Program> FetchById(Guid ProgramId);
		Task<int> DeleteWorkout(Models.Program program);
	}
}

