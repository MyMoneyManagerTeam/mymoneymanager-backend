using System;

namespace Application.Services.Users.Dto
{
    public interface IOutputDtoAuthSign
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public string Token { get; set; }
    }
}