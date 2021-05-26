namespace DataAccess.Models
{
    using System;

    public class Transaction
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid PotId { get; set; }

        public virtual Pot Pot { get; set; }
    }
}
