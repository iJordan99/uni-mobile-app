using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		protected readonly IAppState appState;

		public BaseViewModel(IAppState appState)
		{
			this.appState = appState;
		}
	}
}

