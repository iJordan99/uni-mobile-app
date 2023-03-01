using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class ProgrammesPage : ContentPage
{

    public ProgrammesPage(ProgrammesPageViewModel vm)
    {
        InitializeComponent();   
        this.BindingContext = vm;
    }
}
