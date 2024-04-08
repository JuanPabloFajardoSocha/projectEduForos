using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Subject.Response
{
    public record GetSubjectsResponse
    (
        int IdSubject,
        string NameSubject
        );
}
