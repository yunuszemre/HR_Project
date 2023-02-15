using HR.Project.Entities.Abstract;
using HR.Project.Entities.Enum;

namespace HR.Project.Entities.Concrete
{
    public class AdvancePayment : BaseEntity
    {

        public RequestType RequestType { get; set; }
        public DateTime RequestDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public double RequestAmount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public DateTime? ReplyDate { get; set; }

        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }



    }
}
