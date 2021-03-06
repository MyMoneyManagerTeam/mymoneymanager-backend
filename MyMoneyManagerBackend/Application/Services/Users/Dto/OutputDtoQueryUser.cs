﻿using System;

namespace Application.Services.Users.Dto
{
    public class OutputDtoQueryUser
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public bool Confirmed { get; set; }
        public bool Admin { get; set; }
        public double Balance { get; set; }
    }
}