using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Course.Results
{
    public record GetCoursesByUserResponse
    (
        int idCourse,
        string name,
        string description
        );
}
