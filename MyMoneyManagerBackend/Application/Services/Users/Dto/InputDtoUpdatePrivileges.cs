using System;

namespace Application.Services.Users.Dto
{
    public class InputDtoUpdatePrivileges
    {
        public Guid Id { get; set; }
        public bool Admin { get; set; }
        public bool Confirmed { get; set; }
    }
}