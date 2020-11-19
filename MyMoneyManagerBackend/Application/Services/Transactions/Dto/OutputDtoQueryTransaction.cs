using System;

namespace Application.Services.Transactions.Dto
{
    public class OutputDtoQueryTransaction
    {
        public Guid Id { get; set; }
        public Guid EmitterId { get; set; }
        public Guid ReceiverId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string EmitterName { get; set; }
        public string ReceiverName { get; set; }
    }
}