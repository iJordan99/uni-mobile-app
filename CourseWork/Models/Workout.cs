using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
	[Table("Workouts")]
	public class Workout
	{
        [PrimaryKey, Column("_Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(typeof(User))]
        [NotNull]
        public Guid UserId { get; set; }

        [NotNull]
        public DateTime DateAdded { get; set; } = DateTime.Today;

        [ManyToOne]
        public User User { get; set; }

        public string WorkoutName { get; set; }

        public Workout(User user, string workoutName)
		{
            WorkoutName = workoutName;            
            UserId = user.Id;
		}

        public Workout() { }
	}
}

