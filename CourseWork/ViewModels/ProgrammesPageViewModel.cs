using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
    public partial class ProgrammesPageViewModel : BaseViewModel
    {
        public ProgrammesPageViewModel(IAppState appState) : base(appState)
        {
        }

        [RelayCommand]
        private async Task NavigateToCreateProgrammePage()
        {
            await Shell.Current.GoToAsync("/CreateProgrammePage");
        }

        

    }
}

