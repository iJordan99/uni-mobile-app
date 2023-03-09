using SQLite;
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

