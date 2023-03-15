using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class WorkoutDetailsPage : ContentPage
{ 
    public WorkoutDetailsPage(WorkoutDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
