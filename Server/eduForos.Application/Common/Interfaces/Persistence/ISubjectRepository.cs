using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Common.Interfaces.Persistence
{
    public interface ISubjectRepository
    {
        bool RegisterSubject(Subject subject);
        List<Subject> GetAllSubjects();
        bool DeleteSubject(int idSubject);
        Subject GetSubjectByName(string name);
        Subject GetSubjectById(int id);
     

    }
}
