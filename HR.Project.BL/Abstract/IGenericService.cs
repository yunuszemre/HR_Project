using HR.Project.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HR.Project.BL.Abstract
{
    public interface IGenericService<T> where T : BaseEntity
    {
        public bool Add(T item);
        public bool Update(T item);
        public T GetById(int id);
        public int Save();
        List<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        public bool Any(Expression<Func<T, bool>> predicate);
        public T GetByDefault(Expression<Func<T, bool>> predicate);
    }
}
