namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;

    public class Pot
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal TotalAmount { get; set; } = 0m;

        public bool Activated { get; set; } = true;

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
