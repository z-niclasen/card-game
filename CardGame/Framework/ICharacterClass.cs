using CardGame.Constants;

namespace CardGame.Framework;

public interface ICharacterClass
{
    public CharacterName Name { get; }
    
    public int HandDrawCount { get; }
    
    public bool CanPlayCard(ICard card);

    public void SpendResourcesForCard(ICard card);

    public int AdjustDamageDealt(int damageDealt);
}