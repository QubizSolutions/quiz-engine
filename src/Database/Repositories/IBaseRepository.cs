using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAll();
        Task<TModel> GetByIDAsync(int? id);

        void Create(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
    }
}
