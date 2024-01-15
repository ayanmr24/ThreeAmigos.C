using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.Models.OrderItems;

namespace ThAmCoSystem.Models.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
         public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int StockQuantity { get; set; }
        public string CustomerID { get; set; }
        public ThAmCoSystemUser ThAmCoSystemUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
