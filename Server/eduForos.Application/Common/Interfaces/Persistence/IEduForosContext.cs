using eduForos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IEduForosDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<VideoConference> VideoConferences { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<CourseSubject> CourseSubjects { get; set; }
    public DbSet<UserSubject> UserSubjects { get; set; }
}