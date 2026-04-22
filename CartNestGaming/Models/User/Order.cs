using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartNestGaming.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }   // ✅ FIXED (string → int)

        [ForeignKey("UserId")]
        public AppUser User { get; set; } // ✅ ADD THIS

        public string OrderCode { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // ✅ safe
    }
}