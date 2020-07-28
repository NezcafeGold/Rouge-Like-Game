using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "TileCardData")]
    public class TileCard : ScriptableObject
    {
        public int id;
        public string name;
        public Sprite sprite;
        public TileCardType tileCardWithType;
        public List<StructCardTypeAmount> cardList;


    }
}