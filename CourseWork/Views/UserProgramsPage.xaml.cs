using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class UserProgramsPage : ContentPage
{
	public UserProgramsPage(UserProgramsPageViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}
