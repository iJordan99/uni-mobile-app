using System.Collections.ObjectModel;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services;

public class WorkoutSessionDatabaseServiceDatabaseService: BaseDatabaseService, IWorkoutSessionDatabaseService
{
    public WorkoutSessionDatabaseServiceDatabaseService(SQLiteAsyncConnection database) : base(database)
    {
    }

    public async Task<WorkoutSession> StoreWorkoutSession(WorkoutSession workoutSession)
    {
        await Database.InsertAsync(workoutSession);
        return await Database.Table<WorkoutSession>().Where(m => m.Id == workoutSession.Id).FirstOrDefaultAsync();
    }

    public async Task<ObservableCollection<WorkoutSession>> FetchSessions(User user)
    {
        try
        {
            List<WorkoutSession> sessionsList = await Database.Table<WorkoutSession>().Where(m => m.UserId == user.Id
                && m.SessionDate<= DateTime.Now ).ToListAsync();

            ObservableCollection<WorkoutSession> sessions = new ObservableCollection<WorkoutSession>(sessionsList);

            return sessions;

        } catch(Exception e)
        {
            Console.Write(e.Message);
            return null;
        }
    }
}