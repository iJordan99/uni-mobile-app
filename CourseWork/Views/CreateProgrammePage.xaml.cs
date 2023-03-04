using CourseWork.ViewModels;
namespace CourseWork.Views;

public partial class CreateProgrammePage : ContentPage
{
	public CreateProgrammePage(CreateProgrammePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    void EntryName_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as CreateProgrammePageViewModel);
        viewModel.ProgrammeName = e.NewTextValue ?? string.Empty;
    }
}

