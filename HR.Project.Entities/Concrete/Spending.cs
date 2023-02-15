using HR.Project.Entities.Abstract;
using HR.Project.Entities.Enum;

namespace HR.Project.Entities.Concrete
{
    public class Spending : BaseEntity
    {
        public SpendingType SpendingType { get; set; }
        public DateTime SpendingDate { get; set; }
        public bool Status { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public double SpendingAmount { get; set; }
        public Currency SpendingCurrency { get; set; }
        public string SpendingDescription { get; set; }
        public DateTime? ReplyDate { get; set; }


        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}

