using HR.Project.Entities.Abstract;
using HR.Project.Entities.Enum;

namespace HR.Project.Entities.Concrete
{
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            this.AdvancePayments = new List<AdvancePayment>();
            this.Permissions = new List<Permission>();
            this.Spendings = new List<Spending>();
        }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthLocation { get; set; }
        public string ImageURL { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime JobEnterDate { get; set; }
        public DateTime? JobExitDate { get; set; }
        public bool IsActıve { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public Gender Gender { get; set; }
        public int? YillikIzinSayisi { get; set; }



        // Advance Table Navigation Property
        public int UserRoleId { get; set; }
        public Role UserRole { get; set; }
        public virtual List<AdvancePayment>? AdvancePayments { get; set; }
        public virtual List<Spending>? Spendings { get; set; }
        public virtual List<Permission> Permissions { get; set; }

    }
}
