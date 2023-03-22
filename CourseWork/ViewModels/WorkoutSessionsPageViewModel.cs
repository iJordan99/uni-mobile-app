using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
	public class WorkoutSessionsPageViewModel : BaseViewModel
	{

		private readonly IWorkoutSessionDatabaseService _workoutSessionDb;

		private readonly IWorkoutSessionExerciseDatabaseService _workoutSessionExerciseDb;
		
		public WorkoutSessionsPageViewModel(IAppState appState, IUserDatabaseService userDb, IWorkoutSessionDatabaseService workoutSessionDb, IWorkoutSessionExerciseDatabaseService workoutSessionExerciseDb) : base(appState, userDb)
		{
			this._workoutSessionDb = workoutSessionDb;
			this._workoutSessionExerciseDb = workoutSessionExerciseDb;
		}
	}
}

