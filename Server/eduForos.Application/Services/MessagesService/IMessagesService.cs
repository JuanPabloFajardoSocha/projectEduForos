using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.MessagesService
{
    public interface IMessagesService
    {
        Task<bool> CreateMessage(Message message, IFormFile file);
        bool DeleteMessage(int idMessage);
        List<Message> GetMessagesByForum(int idForum);
    }
}
