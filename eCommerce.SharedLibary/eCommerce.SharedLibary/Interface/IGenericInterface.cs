
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.Interface
{
    public interface IGenericInterface<T> where T : class
    {
        Task<Reponse.Reponse> CreateAsync(T entity);
        Task<Reponse.Reponse> UpdateAsync(T entity);
        Task<Reponse.Reponse> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T , bool>> predicate);
    }
}
