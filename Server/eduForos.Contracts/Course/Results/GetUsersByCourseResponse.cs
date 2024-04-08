using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Course.Results
{
    public record GetUsersByCourseResponse
    (
        Guid IdUser,
        string FirtsName,
        string SurName,
        string DocumentNumber,
        string UserType
    );
}
