using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.UserServices.Delete
{
    public interface IDeleteUsers
    {
        bool DeleteRegisteredUser(Guid IdUser);
    }
}
