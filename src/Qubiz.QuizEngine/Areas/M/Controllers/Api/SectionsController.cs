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
		public async Task<IHttpActionResult> GetSections()
		{
			Section[] sections = await sectionService.GetAllSectionsAsync();

			return Ok(sections);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteSection(Guid id)
		{
			ValidationError[] validationErrors = await sectionService.DeleteSectionAsync(id);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}

		[HttpPost]
		public async Task<IHttpActionResult> AddSection(Section newSection)
		{
			//Section s = new Section();
			//s.Name = newSection;
			//s.ID = Guid.NewGuid();
			ValidationError[] validationErrors = await sectionService.AddSectionAsync(newSection);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}

		[HttpPut]
		public async Task<IHttpActionResult> EditSection(Section newSection)
		{
			//Section s = await sectionService.GetSectionAsync(id);
			//if (s == null)
			//	return BadRequest();
			//s.Name = text;
			ValidationError[] validationErrors = await sectionService.UpdateSectionAsync(newSection);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}
	}
}