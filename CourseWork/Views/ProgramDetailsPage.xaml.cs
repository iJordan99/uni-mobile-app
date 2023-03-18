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
        var workoutExercise = entry?.BindingContext as ProgramExercise;

        if (workoutExercise == null) return;
        if (double.TryParse(e.NewTextValue, out double weight))
        {
            workoutExercise.Weight = Convert.ToDouble(e.NewTextValue);
            return;
        }

        workoutExercise.Weight = 0;
    }
    
    void Sets_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var workoutExercise = entry?.BindingContext as ProgramExercise;

        if (workoutExercise == null) return;
        if (int.TryParse(e.NewTextValue, out int sets))
        {
            workoutExercise.Sets = sets;
            return;
        }
        workoutExercise.Sets = 0;
    }
    
    void Reps_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var workoutExercise = entry?.BindingContext as ProgramExercise;

        if (workoutExercise == null) return;
        if (int.TryParse(e.NewTextValue, out int reps))
        {
            workoutExercise.Reps = reps;
            return;
        }

        workoutExercise.Reps = 0;

    }
}
