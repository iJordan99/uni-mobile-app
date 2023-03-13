using System;
using CourseWork.Interfaces;
using CourseWork.Models;

namespace CourseWork
{
	public class AppState : IAppState
	{
		public User CurrentUser { get; set; }

		public AppState()
		{
		}
	}
}

