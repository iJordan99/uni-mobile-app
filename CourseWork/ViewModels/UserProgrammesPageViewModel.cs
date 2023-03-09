using System;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
	public class UserProgrammesPageViewModel : BaseViewModel
    {
		public UserProgrammesPageViewModel(IAppState appState, IUserDatabaseService userDB) : base(appState, userDB){}
	}
}

