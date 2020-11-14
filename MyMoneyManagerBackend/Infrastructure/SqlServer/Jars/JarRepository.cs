using System;
using System.Collections.Generic;
using Application.Repositories;
using Domain.Jars;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using JarFactory = Infrastructure.SqlServer.Factories.JarFactory;

namespace Infrastructure.SqlServer.Jars
{
    public class JarRepository: IJarRepository
    {
        private IInstanceFromReaderFactory<IJar> _jarFactory = new JarFactory();

        public IEnumerable<IJar> Query(IUser user)
        {
            throw new NotImplementedException();
        }

        public IJar Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IJar Create(IJar jar)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, IJar jar)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}