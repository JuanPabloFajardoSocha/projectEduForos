using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Course.Request
{
    public record GetUsersByCourseRequest
    (
        int idCourse
    );
}
