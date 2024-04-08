using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class UserSubject
{
    public int IdUserSubject { get; set; }

    public Guid IdUser { get; set; }

    public int IdSubject { get; set; }

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
