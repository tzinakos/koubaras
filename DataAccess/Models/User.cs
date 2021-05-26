namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Pot> Pots { get; set; }
    }
}
