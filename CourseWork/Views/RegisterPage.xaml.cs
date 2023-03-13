using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    void Username_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as RegisterPageViewModel);
        viewModel.EntryUsername = e.NewTextValue ?? string.Empty;
    }

    void Email_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as RegisterPageViewModel);
        viewModel.EntryEmail = e.NewTextValue ?? string.Empty;
    }

    void Password_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as RegisterPageViewModel);
        viewModel.EntryPassword = e.NewTextValue ?? string.Empty;
    }
}
