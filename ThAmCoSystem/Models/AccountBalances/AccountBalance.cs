using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThAmCoSystem.Areas.Identity.Data;

namespace ThAmCoSystem.Models.AccountBalances
{
    public class AccountBalance
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountBalanceId { get; set; }
        public string CustomerID { get; set; }
        public ThAmCoSystemUser ThAmCoSystemUser { get; set; }
        public int Balance { get; set; }

    }
}
