using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Database.Repositories;
using Qubiz.QuizEngine.Database.Models;

namespace Qubiz.QuizEngine.Services
{
	public class QuestionService : IQuestionService
	{

		private IConfig config;

		public QuestionService(IConfig config)
		{
			this.config = config;
		}

		public PagedResult<QuestionListItem> GetQuestionsByPage(int pagenumber)
		{
			UnitOfWork unit = new UnitOfWork(config);
			unit.QuestionRepository.GetQuestionsByPage(pagenumber);

			return null;
		}
	}
}
