using eduforos.Domain.Modelos;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using System.Timers;

namespace eduForos.Infrastructure.Persistence;

public class MessageRepository : IMessageRepository
{
    private readonly EduForosDbContext _context;

    public MessageRepository(EduForosDbContext context)
    {
        _context = context;
    }

    public bool CreateMessage(Message message)
    {
        bool status = false;
        try
        {
            Console.WriteLine("Entro a repositorio");
            var foro = _context.Forums.FirstOrDefault(f => f.IdForum == message.IdForum);
            if (foro != null)
            {
                _context.Messages.Add(message);
            }
            else
            {
                throw new Exception("El foro no existe");
            }

            status = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            _context.SaveChanges();
        }
        return status;

    }

    public bool DeleteMessage(int idMessage)
    {
        bool status = false;
        try
        {
            var response = _context.Messages.FirstOrDefault(m => m.IdMessage == idMessage);
            if (response != null)
            {
                _context.Messages.Remove(response);
                status = true;
            }
            else
            {
                throw new Exception("No se pudo eliminar el mensaje");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            _context.SaveChanges();
        }
        return status;
    }

    public List<MessagesWithUsers> GetMessagesByForum(int idForum)
    {
        var messagesWithUserNames = _context.Messages
               .Where(m => m.IdForum == idForum)
            .Select(m => new MessagesWithUsers
                {
                    Message = m,
                    FirtsNameUser = _context.Users.FirstOrDefault(u => u.IdUser == m.IdUser).FirtsName,
                    SurNameUser= _context.Users.FirstOrDefault(u=>u.IdUser==m.IdUser).SurName,
                })
                .ToList();

        return messagesWithUserNames;
    }
}