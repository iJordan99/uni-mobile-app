using CourseWork.Interfaces;
using CourseWork.Services;
using Microsoft.Extensions.Logging;
using SQLite;

namespace CourseWork;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SF-Pro-Rounded-Regular.otf", "SF-Pro-Rounded");
                fonts.AddFont("SF-Pro-Rounded-Bold.otf", "SF-Pro-Bold");
                fonts.AddFont("SF-Pro-Italic.ttf", "SF-Pro-Italic");
            })
            .RegisterViewsAndViewModels()
            .RegisterServices(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
    public static MauiAppBuilder RegisterViewsAndViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        //Pages
        mauiAppBuilder.Services.AddTransient(typeof(Views.HomePage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.ProgressPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.ProgrammesPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.CreateWorkoutPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.UserProgrammesPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.LoginPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.RegisterPage));

        //Services
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.HomePageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.ProgressPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.ProgrammesPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.CreateWorkoutPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.UserProgrammesPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.LoginPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.RegisterPageViewModel));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IAppState, AppState>();

        // Register SQLiteAsyncConnection
        mauiAppBuilder.Services.AddSingleton(provider =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        mauiAppBuilder.Services.AddSingleton<IUserDatabaseService, UserDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IMetricDatabaseService, MetricDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IWorkoutDatabaseService, WorkoutDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IWorkoutExerciseDatabaseService, WorkoutExceriseDatabaseService>();

        return mauiAppBuilder;
    }
}

