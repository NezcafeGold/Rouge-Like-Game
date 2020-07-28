using System;
using UnityEngine;


namespace DefaultNamespace
{
    [Serializable]
    public struct StructCardTypeAmount
    {
        public CardType cardType;
        public CardData cardData;
        public int amount;

        public StructCardTypeAmount(CardType cardType, int amount, CardData cardData)
        {
            this.cardType = cardType;
            this.amount = amount;
            this.cardData = cardData;
        }
    }
}