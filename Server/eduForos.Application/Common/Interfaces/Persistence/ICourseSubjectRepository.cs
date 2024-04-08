using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Common.Interfaces.Persistence
{
    public interface ICourseSubjectRepository
    {
        bool AddCourseToSubject(Course course, Subject subject);
        bool DeleteCourseToSubject(int idCourse, int idSubject);

        List<Subject> GetSubjectsByCourse(int idCourse);

    }
}
