using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Infrastructure;
using Qubiz.QuizEngine.Services.SectionService;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qubiz.QuizEngine.Areas.M.Controllers.Api
{
	[Admin]
	public class SectionController : ApiController
	{
		private readonly ISectionService sectionService;

		public SectionController(ISectionService sectionService)
		{
			this.sectionService = sectionService;
		}

		[HttpGet]
		public async Task<IHttpActionResult> GetSectionsAsync()
		{
			Section[] sections = await sectionService.GetAllSectionsAsync();

			return Ok(sections);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteSectionAsync(Guid id)
		{
			ValidationError[] validationErrors = await sectionService.DeleteSectionAsync(id);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}
	}
}