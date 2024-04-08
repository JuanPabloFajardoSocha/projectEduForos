using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class CourseSubject
{
    public int IdCourseSubject { get; set; }

    public int IdCourse { get; set; }

    public int IdSubject { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
