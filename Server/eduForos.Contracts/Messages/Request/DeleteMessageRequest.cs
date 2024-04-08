using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Messages.Request
{
    public record DeleteMessageRequest
    (
        int idMessage
        );
}
