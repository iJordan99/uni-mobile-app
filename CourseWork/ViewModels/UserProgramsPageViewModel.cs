using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
    public partial class UserProgramsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Models.Program> programmes;

        private readonly IProgramDatabaseService ProgramDb;
       
        public UserProgramsPageViewModel(IAppState appState, IUserDatabaseService userDB, IProgramDatabaseService programDb) : base(appState, userDB)
        {
            this.ProgramDb = programDb;
            Programmes = new ObservableCollection<Models.Program>();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetProgrammes();
        }

        private async Task GetProgrammes()
        {
            Programmes = await ProgramDb.FetchAllByUser(appState.CurrentUser);
        }

        [RelayCommand]
        private async Task ProgrammeInfo(Models.Program program)
        {
            await Shell.Current.GoToAsync($"/ProgramDetailsPage?workoutId={program.Id}");
        }

        [RelayCommand]
        async void Delete(Models.Program program)
        {
            if(Programmes.Contains(program))
            {
                await ProgramDb.DeleteWorkout(program);
                Programmes.Remove(program);
            }
        }
    }
}

