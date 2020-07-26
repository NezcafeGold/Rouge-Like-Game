using System;
using UnityEngine;


namespace DefaultNamespace
{
    [Serializable]
    public struct StructCardTypeAmount
    {
        public CardType cardType;
        public Card card;
        public int amount;

        public StructCardTypeAmount(CardType cardType, int amount, Card card)
        {
            this.cardType = cardType;
            this.amount = amount;
            this.card = card;
        }
    }
}