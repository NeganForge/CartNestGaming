using System.ComponentModel.DataAnnotations;

namespace CartNestGaming.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string OrderCode { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        // Navigation
        public List<OrderItem> OrderItems { get; set; }
    }
}
