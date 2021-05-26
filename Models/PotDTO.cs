namespace Models
{
    using System;

    public class PotDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal? TotalAmount { get; set; } = 0m;

        public bool? Activated { get; set; } = true;

        public Guid UserId { get; set; }
    }
}
