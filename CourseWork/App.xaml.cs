namespace CourseWork;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("LoginPage", typeof(Views.LoginPage));
        Routing.RegisterRoute("ProgressPage", typeof(Views.ProgressPage));
        Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
        Routing.RegisterRoute("HomePage", typeof(Views.HomePage));
        Routing.RegisterRoute("CreateProgrammePage", typeof(Views.CreateProgrammePage));
		Routing.RegisterRoute("ProgrammesPage", typeof(Views.ProgrammesPage));
		Routing.RegisterRoute("UserProgrammesPage", typeof(Views.UserProgrammesPage));
        MainPage = new AppShell();
	}
}

