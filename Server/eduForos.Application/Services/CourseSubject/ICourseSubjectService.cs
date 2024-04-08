using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.CourseSubject
{
    public interface ICourseSubjectService
    {
        bool AddCourseToSubject(int idCourse, int idSubject);
        bool DeleteCourseToSubject(int idCourse, int idSubject);
        List<Subject> GetSubjectsByCourse(int idCourse);    

    }
}
