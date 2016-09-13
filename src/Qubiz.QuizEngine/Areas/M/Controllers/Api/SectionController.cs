using Qubiz.QuizEngine.Areas.M.Models;
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
			Services.SectionService.Contract.Section[] sections = await sectionService.GetAllSectionsAsync();

			return Ok(sections.DeepCopyTo<Section[]>());
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get(Guid id)
		{
			Services.SectionService.Contract.Section section = await sectionService.GetSectionAsync(id);

			return Ok(section.DeepCopyTo<Section>());
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
			ValidationError[] validationErrors = await sectionService.AddSectionAsync(section.DeepCopyTo<Services.SectionService.Contract.Section>());
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}

		[HttpPut]
		public async Task<IHttpActionResult> Put(Section section)
		{
			ValidationError[] validationErrors = await sectionService.UpdateSectionAsync(section.DeepCopyTo<Services.SectionService.Contract.Section>());
			if (validationErrors.Any())
				return BadRequest();

			return Ok();
		}
	}
}