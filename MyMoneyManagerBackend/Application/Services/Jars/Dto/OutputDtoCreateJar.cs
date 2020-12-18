﻿using System;

namespace Application.Services.Jars.Dto
{
    public class OutputDtoCreateJar
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Max { get; set; }
        public double Balance { get; set; }

        protected bool Equals(OutputDtoCreateJar other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((OutputDtoCreateJar) obj);
        }
    }
}