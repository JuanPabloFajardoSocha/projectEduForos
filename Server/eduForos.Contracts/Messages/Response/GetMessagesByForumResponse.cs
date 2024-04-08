using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eduforos.Contracts.Messages.Response
{
    public record GetMessagesByForumResponse
    (
        int idMessage,
        string? message,
        string? urlFile,
        string? assetId,
        DateTime date,
        int? calification,
        int idForum,
        Guid idUser,
        string nameUser
        );
}
