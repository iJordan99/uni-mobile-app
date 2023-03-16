using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
    [Table("Metrics")]
    public class Metric
	{
		public double Weight { get; set; }
		public double Height { get; set; }
		public double BodyFat { get; set; }
		public DateTime Date { get; set; }

        [PrimaryKey, Column("_Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(typeof(User))]
        [NotNull]
        public Guid UserId { get; set; }

        [ManyToOne]
        public User User { get; set; }

        public Metric()
		{
		}

        public Metric(double weight, double height, double bodyFat, DateTime date ,User user)
        {
            Weight = weight;
            Height = height;
            BodyFat = bodyFat;
            Date = date;
            UserId = user.Id;
        }
	}
}

