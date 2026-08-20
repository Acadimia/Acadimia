using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // Platform-level commission revenue earned from each enrollment, for
    // reporting/reconciliation. Not a personal wallet - written by the same
    // service call that creates the instructor's InstructorCredit transaction.
    public class PlatformRevenueLedger : BaseModel
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CommissionAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CommissionRateApplied { get; set; }
    }
}
