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
        }
    }

    void Weight_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (double.TryParse(e.NewTextValue, out double weight))
            {
                viewModel.Weight = weight;
                return;
            }
            DisplayAlert("Invalid Weight", "Please enter a valid number for weight.", "OK");
        }
        viewModel.Weight = 0;
    }

    void Height_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (double.TryParse(e.NewTextValue, out double height))
            {
                viewModel.Height = double.Parse(e.NewTextValue);
                return;
            }
            DisplayAlert("Invalid Height", "Please enter a valid number for height.", "OK");
        }
        viewModel.Height = 0;
    }

    void BodyFat_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as HomePageViewModel);
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (double.TryParse(e.NewTextValue, out double height))
            {
                viewModel.BodyFat = double.Parse(e.NewTextValue);
                return;
            }
            DisplayAlert("Invalid Body Fat", "Please enter a valid number for Body Fat.", "OK");
        }
        viewModel.BodyFat = 0;
    }
}
