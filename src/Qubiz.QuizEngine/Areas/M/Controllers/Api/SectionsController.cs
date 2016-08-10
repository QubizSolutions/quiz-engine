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
		public async Task<IHttpActionResult> Get()
		{
			Section[] sections = await sectionService.GetAllSectionsAsync();

			return Ok(sections);
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get(Guid id)
		{
			Section section = await sectionService.GetSectionAsync(id);

			return Ok(section);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> Delete(Guid id)
		{
			ValidationError[] validationErrors = await sectionService.DeleteSectionAsync(id);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}

		[HttpPost]
		public async Task<IHttpActionResult> Post(Section section)
		{
			ValidationError[] validationErrors = await sectionService.AddSectionAsync(section);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}

		[HttpPut]
		public async Task<IHttpActionResult> Put(Section section)
		{
			ValidationError[] validationErrors = await sectionService.UpdateSectionAsync(section);
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}
	}
}