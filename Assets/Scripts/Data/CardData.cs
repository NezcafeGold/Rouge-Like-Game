using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "CardData")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private CardType cardWithType;
        [SerializeField] private Sprite cardShirt;


        public CardType CardWithType
        {
            get { return cardWithType; }
            set { cardWithType = value; }
        }

        public Sprite CardShirt
        {
            get { return cardShirt; }
            set { cardShirt = value; }
        }
    }
}