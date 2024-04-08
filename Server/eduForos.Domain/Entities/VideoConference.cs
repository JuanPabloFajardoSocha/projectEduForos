using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class VideoConference
{
    public int IdVideoConference { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IdCourse { get; set; }

    public int IdSubject { get; set; }

    public string Link { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
