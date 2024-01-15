using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.Models.Orders;

namespace ThAmCoSystem.Models.OrdersHistory
{
    public class OrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderHistoryId { get; set; }
     
        public string CustomerId { get; set; }
        public ThAmCoSystemUser ThAmCoSystemUser { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string Status { get; set; }
    }
}
