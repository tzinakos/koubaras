namespace Models
{
    using System;

    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    }
}
