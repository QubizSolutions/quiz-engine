using System;
using System.Linq;
using Qubiz.QuizEngine.Database.Entities;

namespace Qubiz.QuizEngine.Repositories
{
    public interface IFeatureFlagRepository
	{
		FeatureFlag[] GetAllFeatureFlags();

		FeatureFlag GetFeatureFlagByID(Guid id);
	}

	public class FeatureFlagRepository : IFeatureFlagRepository
	{
		private readonly IRepository repository;

		public FeatureFlagRepository(IRepository repository)
		{
			this.repository = repository;
		}

		public FeatureFlag[] GetAllFeatureFlags()
		{
			return repository.All<FeatureFlag>().ToArray();
		}

		public FeatureFlag GetFeatureFlagByID(Guid id)
		{
			return repository.GetByID<FeatureFlag>(id);
		}
	}
}