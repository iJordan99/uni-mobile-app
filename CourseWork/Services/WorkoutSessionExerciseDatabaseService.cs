using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services;

public class WorkoutSessionExerciseDatabaseService : BaseDatabaseService, IWorkoutSessionExerciseDatabaseService
{
    public WorkoutSessionExerciseDatabaseService(SQLiteAsyncConnection database) : base(database)
    {
    }

    public async Task<int> StoreWorkoutSessionExercise(WorkoutSessionExercise workoutSessionExercise)
    {
        try
        {
            return await Database.InsertAsync(workoutSessionExercise);
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to store workout session exercise: {e.Message}");
        }
    }

    public async Task<ObservableCollection<WorkoutSessionExercise>> FetchSessionExercises(WorkoutSession workoutSession)
    {
        try
        {
            List<WorkoutSessionExercise> exerciseList = await Database.Table<WorkoutSessionExercise>().Where(
                m => m.SessionId == workoutSession.Id).ToListAsync();

            ObservableCollection<WorkoutSessionExercise> exercises = new ObservableCollection<WorkoutSessionExercise>(exerciseList);

            return exercises;

        } catch(Exception e)
        {
            throw new Exception($"Unable to fetch workout session {workoutSession.Id} exercises: {e.Message}");
        }
    }
}