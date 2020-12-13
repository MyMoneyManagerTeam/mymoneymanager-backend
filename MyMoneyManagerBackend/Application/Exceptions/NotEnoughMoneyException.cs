using System;

namespace Application.Exceptions
{
    public class NotEnoughMoneyException: Exception
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string? message) : base(message)
        {
        }
    }
}