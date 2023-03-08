using System;
using CourseWork.Models;

namespace CourseWork.Interfaces
{
	public interface IAppState
	{
		public User CurrentUser { get; set; }
	}
}

