using CourseWork.ViewModels;
namespace CourseWork.Views;

public partial class CreateWorkoutProgramPage : ContentPage
{
	public CreateWorkoutProgramPage(CreateWorkoutProgramPageViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}

    void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as CreateWorkoutProgramPageViewModel);
        viewModel.ProgramName = e.NewTextValue ?? string.Empty;
    }
}

