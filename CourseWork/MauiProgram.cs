using CourseWork.Interfaces;
using CourseWork.Services;
using Microsoft.Extensions.Logging;


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
        mauiAppBuilder.Services.AddTransient(typeof(Views.ProgrammesPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.CreateProgrammePage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.UserProgrammesPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.LoginPage));
        mauiAppBuilder.Services.AddTransient(typeof(Views.RegisterPage));



        //Services
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.ProgrammesPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.CreateProgrammePageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.UserProgrammesPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.LoginPageViewModel));
        mauiAppBuilder.Services.AddTransient(typeof(ViewModels.RegisterPageViewModel));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {

        mauiAppBuilder.Services.AddSingleton<IAppState, AppState>();
        mauiAppBuilder.Services.AddSingleton<IUserDatabaseService, UserDatabaseService>();

        return mauiAppBuilder;
    }
}

