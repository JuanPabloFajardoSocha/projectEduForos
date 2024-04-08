using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Domain.Modelos
{
    public class MessagesWithUsers
    {
        public Message Message { get; set; }
        public string FirtsNameUser { get; set; }
        public string SurNameUser { get; set; }

    }
}
