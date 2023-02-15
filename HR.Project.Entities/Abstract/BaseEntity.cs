namespace HR.Project.Entities.Abstract
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

    }
}
