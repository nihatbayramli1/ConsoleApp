using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Exceptions
{
    public class NotFoundEx:Exception
    {
        public NotFoundEx(string message) : base(message) { }
               
    }
}
