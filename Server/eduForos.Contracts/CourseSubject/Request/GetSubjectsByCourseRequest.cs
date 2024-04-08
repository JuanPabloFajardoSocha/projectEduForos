using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.CourseSubject.Request
{
    public record GetSubjectsByCourseRequest
    (
        int idCourse
    );
}
