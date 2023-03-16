using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models
{
	[Table("Users")]
	public class User
	{
		public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

		[PrimaryKey, Column("_id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Metric> Metrics { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Program> Workouts { get; set; }

        public User()
		{

		}

        public User(string username, string email, string password)
		{
			Username = username;
            Email = email;
            Password = password;
        }
	}
}

