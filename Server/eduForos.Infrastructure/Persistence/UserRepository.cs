using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eduForos.Infrastructure.Persistence;


public class UserRepository : IUserRepository
{

    private readonly EduForosDbContext _context;


    public UserRepository(EduForosDbContext context)
    {
        _context = context;
    }


    public void Add(User user)
    {
        try
        {
            _context.Users.Add(user);

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine("error en infraestructura----------------> " + ex);
        }
    }

    public User? GetUserByInstitutionalEmail(string institutionalEmail)
    {
        try
        {
            var response = _context.Users.SingleOrDefault(u => u.InstitutionalEmail == institutionalEmail);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("error en infraestructura----------------> " + ex);
            return null;
        }
    }

    public User? GetUserByDocument(string userDocument)
    {
        try
        {
            var response = _context.Users.SingleOrDefault(u => u.UserDocument == userDocument);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("error en infraestructura----------------> " + ex);
            return null;
        }
    }

    public List<User> GetRegisteredUsersByType(string userType)
    {
        try
        {
            return _context.Users.Where(user => user.UserType == userType).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("error en infraestructura----------------> " + ex);
        }

    }

    public void Edit(User user)
    {
        Console.WriteLine("foto de usuario..................." + user.UrlPhoto + "contraseña .............." + user.Password);
       
        try
        {
            var result = _context.Users.SingleOrDefault(u => u.IdUser == user.IdUser);

           
            if (result == null)
            {
                throw new Exception("El usuario no existe");
            }
            else
            {
                if (user.UrlPhoto.IsNullOrEmpty())
                {
                    user.UrlPhoto = result.UrlPhoto;
                }

                if (user.Password == "null")
                {
                    user.Password = result.Password;
                }

                Console.WriteLine("esta en el metodo de editars");
                _context.Entry(result).State = EntityState.Detached;

                _context.Users.Update(user);
                _context.SaveChanges();
            }

        }
        catch (Exception ex)
        {
            throw new Exception("error en infraestructura----------------> " + ex);
        }
    }


    public Boolean DeleteUser(Guid IdUser)
    {
        Boolean response = false;
        try
        {
            var UserDelete = _context.Users.FirstOrDefault(u => u.IdUser == IdUser);
            if (UserDelete != null)
            {
                _context.Users.Remove(UserDelete);
                response = true;
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex);
        }
        finally
        {
            _context.SaveChanges();
        }

        return response;
    }

    public User? GetUserById(Guid IdUser)
    {
        try
        {
            return _context.Users.FirstOrDefault(u => u.IdUser == IdUser);
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex);
        }
    }


    public User? SearchRecoverPassword(User user)
    {
        try
        {
            var response = _context.Users.FirstOrDefault(u => u.InstitutionalEmail == user.InstitutionalEmail ||
                u.PersonalEmail == user.PersonalEmail && u.UserDocument == user.UserDocument &&
                u.UserDocumentType == user.UserDocumentType);
            if (response != null)
            {
                return response;
            }
            else
            {
                throw new Exception("No se encontro el usuario");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex);
        }
    }
}