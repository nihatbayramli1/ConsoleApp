using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Implements
{
    public class GroupRepository : IRepository<Group>

    {
        public void Create(Group data)
        {
            try
            {
                if (data is null) throw new NotFoundEx("Data not found!");
                AppDbContext<Group>.datas.Add(data);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void Delete(Group data)
        {
            throw new NotImplementedException();
        }

        public Group Get(Predicate<Group> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll(Predicate<Group> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Group data)
        {
            throw new NotImplementedException();
        }
    }
}
