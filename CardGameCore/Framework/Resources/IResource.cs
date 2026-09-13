using CardGameCore.Constants;

namespace CardGameCore.Framework.Resources;

public interface IResource
{
    public ResourceType ResourceType { get; }
    
    public int Amount { get; }
}