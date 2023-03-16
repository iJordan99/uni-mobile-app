using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    void DatePicker_DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        viewModel.CanUpdate = true;

        if (DateTime.Compare(e.NewDate, e.OldDate) != 0)
        {
            viewModel.Date = e.NewDate;
            //https://learn.microsoft.com/en-us/answers/questions/918723/how-do-i-call-a-method-that-is-in-the-viewmodel-fo
        }
    }

    void Weight_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            viewModel.Weight = double.Parse(e.NewTextValue);
            return;
        }
        viewModel.Weight = 0;
    }

    void Height_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            viewModel.Height = double.Parse(e.NewTextValue);
            return;
        }
        viewModel.Height = 0;
    }

    void BodyFat_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            viewModel.BodyFat = double.Parse(e.NewTextValue);
            return;
        }
        viewModel.BodyFat = 0;
    }
}
