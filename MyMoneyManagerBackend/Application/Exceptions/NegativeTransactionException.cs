using System;

namespace Application.Exceptions
{
    public class NegativeTransactionException : Exception
    {
        public NegativeTransactionException(string? message) : base(message)
        {
            
        }
    }
}