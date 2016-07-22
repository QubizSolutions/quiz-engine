using Qubiz.QuizEngine.Database.Entities;
using Qubiz.QuizEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Qubiz.QuizEngine.Controllers
{
    public class FeatureFlagController : ApiController
	{
		private readonly IFeatureFlagRepository featureFlagRepository;

		public FeatureFlagController(IFeatureFlagRepository repository)
		{
			this.featureFlagRepository = repository;
		}

		[Route("api/FeatureFlag")]
		public IEnumerable<FeatureFlag> Get()
		{
			return featureFlagRepository.GetAllFeatureFlags();
		}

		[Route("api/FeatureFlag/id")]
		public FeatureFlag Get(Guid id)
		{
			return featureFlagRepository.GetFeatureFlagByID(id);
		}
	}
}