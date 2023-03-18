using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		protected readonly IAppState AppState;
		protected readonly IUserDatabaseService UserDb;

		protected BaseViewModel(IAppState appState, IUserDatabaseService userDb)
		{
			this.AppState = appState;
			this.UserDb = userDb;
		}
	}
}

