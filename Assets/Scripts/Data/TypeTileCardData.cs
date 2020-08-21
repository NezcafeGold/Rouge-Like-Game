using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "TypeTileCardData")]
    public class TypeTileCardData : ScriptableObject
    {
        public int id;
        public string name;
        public TileCardType tileCardWithType;
        public List<StructCardTypeAmount> cardList;
        public Sprite openedTile;
        public Sprite visitedTile;
    }
}