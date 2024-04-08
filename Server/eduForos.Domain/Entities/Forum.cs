using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class Forum
{
    public int IdForum { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }    

    public string? UrlPhoto { get; set; }
    
    public string? AssetId { get; set; }
    
    public int IdCourse { get; set; }

    public int IdSubject { get; set; }
    public Guid IdUser { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Course IdCourseNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
