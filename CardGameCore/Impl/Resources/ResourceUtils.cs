using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Resources;

namespace CardGameCore.Impl.Resources;

public static class ResourceUtils
{
    public static Dictionary<ResourceType, IResourceMutable> StandardResources(int maxHealth, int energyBaseline)
    {
        return new Dictionary<ResourceType, IResourceMutable>
        {
            { ResourceType.Health, new HealthResource(maxHealth) },
            { ResourceType.Energy, new EnergyResource(energyBaseline) }
        };
    }
}