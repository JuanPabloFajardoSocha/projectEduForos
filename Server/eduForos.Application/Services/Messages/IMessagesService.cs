using eduforos.Domain.Modelos;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.Messages
{
    public interface IMessagesService
    {
        Task<bool> AddMessage(Message message, IFormFile? file);
        List<MessagesWithUsers> GetMessagesByForum(int idForum);

        bool DeleteMessage(int idMessage);
    }
}
