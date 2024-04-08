using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eduForos.Infrastructure.Context;

public partial class EduForosDbContext : DbContext, IEduForosDbContext
{
    public EduForosDbContext()
    {
    }

    public EduForosDbContext(DbContextOptions<EduForosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseSubject> CourseSubjects { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    public virtual DbSet<UserSubject> UserSubjects { get; set; }

    public virtual DbSet<VideoConference> VideoConferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.IdAnswer);

            entity.ToTable("answer");

            entity.Property(e => e.IdAnswer).HasColumnName("idAnswer");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdMessage).HasColumnName("idMessage");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Route)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("route");

            entity.HasOne(d => d.IdMessageNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.IdMessage)
                .HasConstraintName("FK_answer_message");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_answer_user");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.IdCourse);

            entity.ToTable("course");

            entity.Property(e => e.IdCourse).HasColumnName("idCourse");
            entity.Property(e => e.CreateAt)
                .HasColumnType("date")
                .HasColumnName("create_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("date")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<CourseSubject>(entity =>
        {
            entity.HasKey(e => e.IdCourseSubject);

            entity.ToTable("courseSubject");

            entity.Property(e => e.IdCourseSubject).HasColumnName("idCourseSubject");
            entity.Property(e => e.IdCourse).HasColumnName("idCourse");
            entity.Property(e => e.IdSubject).HasColumnName("idSubject");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.CourseSubjects)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_courseSubject_course");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.CourseSubjects)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_courseSubject_subject");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.IdForum);

            entity.ToTable("forum");

            entity.Property(e => e.IdForum).HasColumnName("idForum");
            entity.Property(e => e.CreateAt)
                .HasColumnType("date")
                .HasColumnName("create_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.IdCourse).HasColumnName("idCourse");
            entity.Property(e => e.IdSubject).HasColumnName("idSubject");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
                        
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name"); 
            entity.Property(e => e.AssetId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("assetId");
            entity.Property(e => e.UrlPhoto)
               .HasMaxLength(250)
               .IsUnicode(false)
               .HasColumnName("urlPhoto");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("date")
                .HasColumnName("update_at");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_forum_course");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_forum_subject");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Forums)
               .HasForeignKey(d => d.IdUser)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_forum_user");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.IdMessage);

            entity.ToTable("message");

            entity.Property(e => e.IdMessage).HasColumnName("idMessage");
            entity.Property(e => e.Calification).HasColumnName("calification");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdForum).HasColumnName("idForum");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Message1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.UrlFile)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("urlFile");
            entity.Property(e => e.AssetId)
               .HasMaxLength(250)
               .IsUnicode(false)
               .HasColumnName("assetId");
            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_forum");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_user");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.IdSubject);

            entity.ToTable("subject");

            entity.Property(e => e.IdSubject).HasColumnName("idSubject");
            entity.Property(e => e.CreateAt)
                .HasColumnType("date")
                .HasColumnName("create_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("date")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK_user_1");

            entity.ToTable("user");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("idUser");
            entity.Property(e => e.AssetId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("AssetId");
            entity.Property(e => e.Age)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("age");
            entity.Property(e => e.CreateAt)
                .HasColumnType("date")
                .HasColumnName("create_at");
            entity.Property(e => e.FirtsName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firtsName");
            entity.Property(e => e.InstitutionalEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("institutionalEmail");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PersonalEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("personalEmail");
            entity.Property(e => e.UrlPhoto)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("UrlPhoto");
            entity.Property(e => e.Profession)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("profession");
            entity.Property(e => e.SurName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("surName");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("date")
                .HasColumnName("update_at");
            entity.Property(e => e.UserDocument)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userDocument");
            entity.Property(e => e.UserDocumentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userDocumentType");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userType");
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.IdUserCourse);

            entity.ToTable("userCourse");

            entity.Property(e => e.IdUserCourse).HasColumnName("idUserCourse");
            entity.Property(e => e.IdCourse).HasColumnName("idCourse");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userCourse_course");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserCourses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userCourse_user");
        });

        modelBuilder.Entity<UserSubject>(entity =>
        {
            entity.HasKey(e => e.IdUserSubject);

            entity.ToTable("userSubject");

            entity.Property(e => e.IdUserSubject).HasColumnName("idUserSubject");
            entity.Property(e => e.IdSubject).HasColumnName("idSubject");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userSubject_subject");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSubjects)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userSubject_user");
        });

        modelBuilder.Entity<VideoConference>(entity =>
        {
            entity.HasKey(e => e.IdVideoConference);

            entity.ToTable("videoConference");

            entity.Property(e => e.IdVideoConference).HasColumnName("idVideoConference");
            entity.Property(e => e.CreateAt)
                .HasColumnType("date")
                .HasColumnName("create_at");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.IdCourse).HasColumnName("idCourse");
            entity.Property(e => e.IdSubject).HasColumnName("idSubject");
            entity.Property(e => e.Link)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("date")
                .HasColumnName("update_at");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.VideoConferences)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_videoConference_course");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.VideoConferences)
                .HasForeignKey(d => d.IdSubject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_videoConference_subject");
        });

    }

}
