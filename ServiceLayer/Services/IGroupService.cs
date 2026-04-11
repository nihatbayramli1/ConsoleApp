using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IGroupService
    {
        Group Create(Group group);
        Group Update(int id, Group group);
        void Delete(int id);
        Group GetById(int id);
    }
}
