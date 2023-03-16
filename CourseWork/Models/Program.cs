using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
	[Table("Programs")]
	public class Program
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

        public string ProgramName { get; set; }

        public Program(User user, string programName)
		{
            ProgramName = programName;            
            UserId = user.Id;
		}

        public Program() { }
	}
}

