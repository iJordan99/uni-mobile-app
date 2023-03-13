using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseWork.Interfaces;

namespace CourseWork.ViewModels
{
    public partial class CreateProgrammePageViewModel : BaseViewModel
	{
		[ObservableProperty]
		ObservableCollection<string> exerciseList;

		[ObservableProperty]
		string excersise;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string programmeName;

        [ObservableProperty]
        string currentUser;

        public CreateProgrammePageViewModel(IAppState appState, IUserDatabaseService userDB) : base(appState, userDB)
        {
            ExerciseList = new ObservableCollection<string>();
            CurrentUser = appState.CurrentUser.Username;
        }

        [RelayCommand]
		void Add()
		{
			if (string.IsNullOrWhiteSpace(Excersise))
				return;

			ExerciseList.Add(Excersise);
			Excersise = string.Empty;
		}

		[RelayCommand]
		void Delete(string s)
		{
			if (ExerciseList.Contains(s))
			{
				ExerciseList.Remove(s);
			}
		}

        [RelayCommand(CanExecute = nameof(CanCreate))]
        void Create()
        {
            Console.WriteLine($"Hello{ProgrammeName}");
        }

        private bool CanCreate()
        {
			return !string.IsNullOrEmpty(ProgrammeName);
		}

    }
}

