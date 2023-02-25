using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CourseWork.ViewModels
{
    public partial class WorkoutPageViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task NavigateToCreateWorkoutPage()
        {
            await Shell.Current.GoToAsync("/CreateWorkoutPage");
        }
    }
}

