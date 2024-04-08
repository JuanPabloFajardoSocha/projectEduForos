using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Contracts.Messages.Request
{
    public record CreateMessageRequest
    (        
        string? message,        
        string date,
        int? calification,
        int idForum,
        Guid idUser     
        
        );
}
