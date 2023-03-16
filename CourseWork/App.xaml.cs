namespace CourseWork;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("LoginPage", typeof(Views.LoginPage));
        Routing.RegisterRoute("ProgressPage", typeof(Views.ProgressPage));
        Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
        Routing.RegisterRoute("ProgramDetailsPage", typeof(Views.ProgramDetailsPage));
        Routing.RegisterRoute("HomePage", typeof(Views.HomePage));
        Routing.RegisterRoute("CreateWorkoutProgramPage", typeof(Views.CreateWorkoutProgramPage));
		Routing.RegisterRoute("UserProgramsPage", typeof(Views.UserProgramsPage));
        MainPage = new AppShell();
	}
}

