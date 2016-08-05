using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public class OptionRepository : BaseRepository<OptionDefinition>, IOptionRepository
    {
        public OptionRepository(QuizEngineDataContext context, UnitOfWork unitOfWork)
            : base(context, unitOfWork)
        { }

        public void DeleteOptions(OptionDefinition[] options)
        {
			foreach(var option in options)
			{
				dbSet.Remove(option);
			}
		}

        public Task<OptionDefinition[]> GetOptionsByQuestionIDs(Guid[] ids)
        {
			/*List<OptionDefinition> list = new List<OptionDefinition>();
			foreach (var id in ids)
			{
				list.Add(dbSet.Where(i => i.QuestionID == id).ToList()[0]);
			}
			return list.ToArray();*/
			return null; 
		}

        public void UpdateOptions(Guid questionID, OptionDefinition[] options)
        {
            throw new NotImplementedException();
        }
    }
}