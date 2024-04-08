
using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.SubjectService
{
    public interface ISubjectService
    {
        bool RegisterSubject(string name);
        List<Subject> GetSubjects();
        bool DeleteSubject(int idSubject);
       

    }
}
