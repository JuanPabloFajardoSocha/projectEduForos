using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class Subject
{
    public int IdSubject { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();

    public virtual ICollection<VideoConference> VideoConferences { get; set; } = new List<VideoConference>();
}
