namespace Models
{
    using System;

    public class TransactionDTO
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public decimal? Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid PotId { get; set; }
    }
}
