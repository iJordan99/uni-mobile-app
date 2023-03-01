namespace CourseWork;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("CreateProgrammePage", typeof(Views.CreateProgrammePage));
		Routing.RegisterRoute("ProgrammesPage", typeof(Views.ProgrammesPage));
        MainPage = new AppShell();
	}
}

