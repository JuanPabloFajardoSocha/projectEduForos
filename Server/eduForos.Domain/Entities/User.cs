using System;
using System.Collections.Generic;

namespace eduForos.Domain.Entities;

public partial class User
{
    public Guid IdUser { get; set; }

    public string UserDocumentType { get; set; } = null!;

    public string UserDocument { get; set; } = null!;

    public string? UrlPhoto { get; set; }

    public string? AssetId { get; set; }
    
    public string FirtsName { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string Age { get; set; } = null!;


    public string Telephone { get; set; } = null!;

    public string InstitutionalEmail { get; set; } = null!;

    public string? PersonalEmail { get; set; }

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();
    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();
}
