using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		protected readonly IAppState appState;
		protected readonly IUserDatabaseService userDB;

		public BaseViewModel(IAppState appState, IUserDatabaseService userDB)
		{
			this.appState = appState;
			this.userDB = userDB;
		}
	}
}

