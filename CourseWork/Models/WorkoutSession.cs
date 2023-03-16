using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CourseWork.Models;

[Table("WorkoutSessions")]
public class WorkoutSession
{
    [PrimaryKey,Column("_Id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey(typeof(User))] public Guid UserId { get; set; }

    public DateTime SessionDate { get; set; }

    public WorkoutSession(Guid userId, DateTime sessionDate)
    {
        UserId = userId;
        SessionDate = sessionDate;
    }

    public WorkoutSession()
    {
    }
}