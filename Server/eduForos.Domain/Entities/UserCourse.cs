using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class UserCourse
{
    public int IdUserCourse { get; set; }

    public Guid IdUser { get; set; }

    public int IdCourse { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
