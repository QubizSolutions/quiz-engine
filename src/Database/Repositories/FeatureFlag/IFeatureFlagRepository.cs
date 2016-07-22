using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qubiz.QuizEngine.Database.Repositories
{
    public interface IFeatureFlagRepository
    {
        Task<FeatureFlag[]> GetAllFeatureFlags();
        Task<FeatureFlag> GetFeatureFlagByID(Guid id);
    }
}
