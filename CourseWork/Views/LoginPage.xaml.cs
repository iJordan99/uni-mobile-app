using CourseWork.ViewModels;
namespace CourseWork.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    void Username_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as LoginPageViewModel);
        viewModel.EntryUsername = e.NewTextValue ?? string.Empty;
    }

    void Password_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as LoginPageViewModel);
        viewModel.EntryPassword = e.NewTextValue ?? string.Empty;
    }
}
