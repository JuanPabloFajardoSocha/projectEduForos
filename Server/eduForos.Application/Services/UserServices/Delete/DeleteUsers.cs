using eduForos.Application.Common.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.UserServices.Delete
{
    internal class DeleteUsers : IDeleteUsers
    {
        private readonly IUserRepository _userRepository;

        public DeleteUsers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool DeleteRegisteredUser(Guid IdUser)
        {
            return _userRepository.DeleteUser(IdUser);
        }
    }
}
