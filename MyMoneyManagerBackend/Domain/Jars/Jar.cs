using System;
using Domain.Users;

namespace Domain.Jars
{
    public class Jar: IJar
    {
        public Jar()
        {
        }

        public Guid Id { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Max { get; set; }
        public double Balance { get; set; }

        protected bool Equals(Jar other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((Jar) obj);
        }
        
    }
}