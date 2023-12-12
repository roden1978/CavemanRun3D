using System.Collections.Generic;
using System.Threading.Tasks;
using StaticData;

namespace HalfDiggers.Runner
{
    public interface IPatternService
    {
        Task Initialize();
        IEnumerable<SpawnPatternStaticData> GetPatternsGroup(PatternGroups patternGroup);
        SpawnPatternStaticData GetRandomPattern();
    }
}