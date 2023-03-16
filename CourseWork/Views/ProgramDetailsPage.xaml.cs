using CourseWork.Models;
using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class ProgramDetailsPage : ContentPage
{ 
    public ProgramDetailsPage(ProgramDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    void Weight_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var WorkoutExercise = entry?.BindingContext as ProgramExercise;

        if (WorkoutExercise != null)
        {
            WorkoutExercise.Weight = Convert.ToDouble(e.NewTextValue);
        }
    }
}
