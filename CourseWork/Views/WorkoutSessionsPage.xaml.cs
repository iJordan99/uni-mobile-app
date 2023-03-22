using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class WorkoutSessionsPage : ContentPage
{
	public WorkoutSessionsPage(WorkoutSessionsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
