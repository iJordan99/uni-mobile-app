using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
	[Table("Workout_Exercises")]
	public class WorkoutExercise
	{

        [PrimaryKey, Column("_Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(typeof(Workout))]
        [NotNull]
        public Guid WorkoutId { get; set; }

        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

        public WorkoutExercise(Workout workout, string exerciseName, int sets, int reps, double weight)
		{
            WorkoutId = workout.Id;
            ExerciseName = exerciseName;
            Sets = sets;
            Reps = reps;
            Weight = weight;
		}

        public WorkoutExercise() { }
    }
}

