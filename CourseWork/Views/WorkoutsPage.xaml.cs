using CourseWork.ViewModels;
using CourseWork.Views;

namespace CourseWork.Views;


public partial class WorkoutsPage : ContentPage
{
    public WorkoutsPage(WorkoutPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
    //public WorkoutsPage()
    //{
    //    InitializeComponent();
    //}
}
