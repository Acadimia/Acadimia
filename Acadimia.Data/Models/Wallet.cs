using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // One balance-holding wallet per platform user (student or instructor).
    // CreatedOn/UpdatedOn/soft-delete audit fields come from BaseModel.
    public class Wallet : BaseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}
