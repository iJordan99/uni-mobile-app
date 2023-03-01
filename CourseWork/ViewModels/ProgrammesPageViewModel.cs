using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CourseWork.ViewModels
{
    public partial class ProgrammesPageViewModel : ObservableObject
    {

        //private string[][,] exercise;

        [RelayCommand]
        private async Task NavigateToCreateProgrammePage()
        {
            await Shell.Current.GoToAsync("/CreateProgrammePage");
        }

        

    }
}

