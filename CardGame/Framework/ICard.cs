using CardGame.Constants;

namespace CardGame.Framework;

public interface ICard
{
    public string Name { get; }
    
    public IEffect Effect { get; }
    
    public string Description { get; }
    
    public int Cost { get; }
    
    public Rarity Rarity { get; }
}