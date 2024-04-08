using eduforos.Domain.Modelos;
using eduForos.Domain.Entities;

namespace eduForos.Application.Common.Interfaces.Persistence;

public interface IMessageRepository
{
    bool DeleteMessage(int idMessage);
    bool CreateMessage(Message message);
    List<MessagesWithUsers> GetMessagesByForum(int idForum);
    
}