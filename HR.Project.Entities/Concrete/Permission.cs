using HR.Project.Entities.Abstract;
using HR.Project.Entities.Enum;

namespace HR.Project.Entities.Concrete
{
    public class Permission : BaseEntity
    {

        public DateTime? PermissionStartDate { get; set; }
        public DateTime? ReplyDate { get; set; }
        public PermissionType PermissionType { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime PermissionEndDate { get; set; }
        public int PermissionDays { get; set; }


        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }



    }
}
