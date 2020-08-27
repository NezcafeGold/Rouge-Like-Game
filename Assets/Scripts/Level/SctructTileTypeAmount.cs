using System;
using DefaultNamespace;


[Serializable]
public struct SctructTileTypeAmount
{
    public TypeTileCardData typeTileCardData;
    public int amount;

    public SctructTileTypeAmount(TypeTileCardData typeTileCardData, int amount)
    {
        this.typeTileCardData = typeTileCardData;
        this.amount = amount;
    }
}