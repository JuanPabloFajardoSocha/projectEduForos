using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class Course
{
    public int IdCourse { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<VideoConference> VideoConferences { get; set; } = new List<VideoConference>();
}
