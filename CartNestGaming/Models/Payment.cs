using System;

namespace CartNestGaming.Models
{
    public class Payment
    {
        public int Id { get; set; }   // Primary Key

        public int UserId { get; set; }
        public int ProductId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public string Status { get; set; }
    }
}