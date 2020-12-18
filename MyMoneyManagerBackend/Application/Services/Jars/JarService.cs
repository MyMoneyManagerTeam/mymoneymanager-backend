using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Jars.Dto;
using Domain.Jars;
using Domain.Users;

namespace Application.Services.Jars
{
    public class JarService : IJarService
    {
        private readonly IJarRepository _jarRepository;
        private readonly IJarFactory _jarFactory = new JarFactory();

        public JarService(IJarRepository jarRepository)
        {
            _jarRepository = jarRepository;
        }

        public IEnumerable<OutputDtoQueryJar> Query(Guid userId)
        {
            return _jarRepository
                .Query(userId)
                .Select(jar => new OutputDtoQueryJar
                {
                    Id = jar.Id,
                    Owner = jar.Owner.Id,
                    Balance = jar.Balance,
                    Description = jar.Description,
                    Max = jar.Max,
                    Name = jar.Name
                });
        }

        public OutputDtoQueryJar Get(Guid userId, Guid jarId)
        {
            var res = _jarRepository.Get(userId, jarId);
            return new OutputDtoQueryJar
            {
                Balance = res.Balance,
                Description = res.Description,
                Id = res.Id,
                Owner = res.Owner.Id,
                Max = res.Max,
                Name = res.Name
            };
        }

        public OutputDtoCreateJar Create(Guid userId,InputDtoCreateJar jar)
        {
            var jarFromDto = _jarFactory.GetFromParam(new User {Id = userId}, new Guid(), jar.Description, jar.Name, jar.Max, jar.Balance);
            var jarInDb = _jarRepository.Create(jarFromDto);
            if (jarInDb == null)
                return null;
            return new OutputDtoCreateJar
            {
                Id = jarInDb.Id,
                Balance = jarInDb.Balance,
                Description = jarInDb.Description,
                Max = jarInDb.Max,
                Name = jarInDb.Name,
                Owner = jarInDb.Owner.Id
            };
        }

        public bool Update(Guid userId, InputDtoUpdateJar jar)
        {
            var jarFromDto = _jarFactory.GetFromParam(new User {Id = userId},jar.Id,jar.Description, jar.Name, jar.Max, jar.Balance);
            return _jarRepository.Update(jar.Id, jarFromDto);
        }

        public bool Delete(Guid userId, Guid jarId)
        {
            return _jarRepository.Delete(userId,jarId);
        }
    }
}