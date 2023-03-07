using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class UserProgrammesPage : ContentPage
{
	public UserProgrammesPage(UserProgrammesPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}
