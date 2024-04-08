using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Forum.Request
{
    public record GetForumsByUser
    (
        Guid idUser
        );
    
}
