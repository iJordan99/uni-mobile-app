using CourseWork.ViewModels;
namespace CourseWork.Views;

public partial class CreateWorkoutPage : ContentPage
{
	public CreateWorkoutPage(CreateWorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    void EntryName_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as CreateWorkoutPageViewModel);
        viewModel.WorkoutName = e.NewTextValue ?? string.Empty;
    }
}

