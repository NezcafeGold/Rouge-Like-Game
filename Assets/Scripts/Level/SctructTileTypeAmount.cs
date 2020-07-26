using System;
using DefaultNamespace;


[Serializable]
public struct SctructTileTypeAmount
{
    public TileCard tileCard;
    public int amount;

    public SctructTileTypeAmount(TileCard tileCard, int amount)
    {
        this.tileCard = tileCard;
        this.amount = amount;
    }
}