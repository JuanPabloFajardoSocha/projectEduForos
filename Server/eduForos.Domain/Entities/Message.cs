using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class Message
{
    public int IdMessage { get; set; }

    public string? Message1 { get; set; }

    public string? UrlFile { get; set; } 

    public string? AssetId { get; set; }

    public DateTime Date { get; set; }

    public int? Calification { get; set; }

    public int IdForum { get; set; }

    public Guid IdUser { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Forum IdForumNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
