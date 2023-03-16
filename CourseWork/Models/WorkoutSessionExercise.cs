using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models;

[Table("WorkoutSessionExercises")]
public class WorkoutSessionExercise
{
    [PrimaryKey, Column("_Id")] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey(typeof(WorkoutSession))]
    [NotNull]
    public Guid SessionId { get; set; }

    public string ExerciseName { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }

    public WorkoutSessionExercise(WorkoutSession workoutSession, string exerciseName, int sets, int reps, double weight)
    {
        SessionId = workoutSession.Id;
        ExerciseName = exerciseName;
        Sets = sets;
        Reps = reps;
        Weight = weight;
    }

    public WorkoutSessionExercise()
    {
        
    }
}