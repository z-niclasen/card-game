namespace CardGameCore.Framework.CombatEncounter;

public interface ICombatCardCollectionMutable : ICombatCardCollection
{ 
    public void DrawCard();

    public void DrawNCards(int numberOfCards);

    public void DiscardCardFromHand(ICard cardToDiscard);

    public void DiscardHand();

    public void ExhaustCardFromHand(ICard cardToExhaust);
}