using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class Answer
{
    public int IdAnswer { get; set; }

    public string Message { get; set; } = null!;

    public string Route { get; set; } = null!;

    public DateTime? Date { get; set; }

    public int? IdMessage { get; set; }

    public Guid? IdUser { get; set; }

    public virtual Message? IdMessageNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
