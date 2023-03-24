using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class ProgressPage : ContentPage
{
    public ProgressPage(ProgressPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


    void DateFromPicker_DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
        var viewModel = (BindingContext as ProgressPageViewModel);
        if (DateTime.Compare(e.NewDate, e.OldDate) != 0)
        {
            viewModel.DateFrom = e.NewDate;
        }
    }
}
