using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Authentication
{
    public record LoginResponse
    (
        Guid IdUser,
        string UserType,
        string Token
    );
}
