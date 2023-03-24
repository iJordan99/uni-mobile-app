using CourseWork.Interfaces;
using CourseWork.Services;
using Microsoft.Extensions.Logging;
using SQLite;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using SkiaSharp.Views.Maui.Controls.Hosting; 
namespace CourseWork;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.UseSkiaSharp(true)
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
        mauiAppBuilder.Services.AddTransient(typeof(Views.CreateWorkoutProgramPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.UserProgramsPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.LoginPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.RegisterPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.ProgramDetailsPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.WorkoutSessionsPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.WorkoutSessionDetailsPage));

        //Services
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.HomePageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.ProgressPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.CreateWorkoutProgramPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.UserProgramsPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.LoginPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.RegisterPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.ProgramDetailsPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.WorkoutSessionsPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.WorkoutSessionDetailsPageViewModel));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IAppState, AppState>();

        // Register SQLiteAsyncConnection
        mauiAppBuilder.Services.AddSingleton(_ => new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));

        mauiAppBuilder.Services.AddSingleton<IUserDatabaseService, UserDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IMetricDatabaseService, MetricDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IProgramDatabaseService, ProgramDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IProgramExerciseDatabaseService, ProgramExceriseDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IWorkoutSessionDatabaseService, WorkoutSessionDatabaseServiceDatabaseService>();
        mauiAppBuilder.Services.AddSingleton<IWorkoutSessionExerciseDatabaseService, WorkoutSessionExerciseDatabaseService>();

        return mauiAppBuilder;
    }
}

