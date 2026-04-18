using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartNestGaming.Models
{
    public class Cart
    {
        [Key] // Primary Key
        public int Id { get; set; }

        public string UserId { get; set; }  // later for login system

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}