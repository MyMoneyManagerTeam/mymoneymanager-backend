using System;
using System.Collections.Generic;
using Application.Services.Jars.Dto;

namespace Application.Services.Jars
{
    public interface IJarService
    {
        IEnumerable<OutputDtoQueryJar> Query(Guid userId);
        OutputDtoCreateJar Create(Guid userId,InputDtoCreateJar jar);
        bool Update(Guid userId, InputDtoUpdateJar jar);
        bool Delete(Guid userId, Guid jarId);
    }
}