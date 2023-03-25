using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
	[Table("ProgramExercises")]
	public class ProgramExercise
	{

        [PrimaryKey, Column("_Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(typeof(WorkoutProgram))]
        [NotNull]
        public Guid WorkoutId { get; set; }

        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }

        public ProgramExercise(WorkoutProgram workoutProgram, string exerciseName, int sets, int reps, double weight)
		{
            WorkoutId = workoutProgram.Id;
            ExerciseName = exerciseName;
            Sets = sets;
            Reps = reps;
            Weight = weight;
		}

        public ProgramExercise() { }
    }
}

