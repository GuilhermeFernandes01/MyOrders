using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOrders.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public bool Paid { get; set; }

        public bool Shipped { get; set; }
    }
}