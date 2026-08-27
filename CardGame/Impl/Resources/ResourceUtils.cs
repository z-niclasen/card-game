using CardGame.Constants;
using CardGame.Framework;

namespace CardGame.Impl.Resources;

public static class ResourceUtils
{
    public static Dictionary<ResourceType, IResource> StandardResources(int maxHealth, int energyBaseline)
    {
        return new Dictionary<ResourceType, IResource>
        {
            { ResourceType.Health, new HealthResource(maxHealth) },
            { ResourceType.Energy, new EnergyResource(energyBaseline) }
        };
    }
}