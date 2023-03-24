using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class WorkoutSessionDetailsPage : ContentPage
{
	public WorkoutSessionDetailsPage(WorkoutSessionDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
