using UnityEngine;

namespace Card
{
    public abstract class Card
    {
        [SerializeField] private GameObject faceCard;
        [SerializeField] private GameObject shirtCard;
        [SerializeField] private GameObject description;
        
        
    }
}


public interface IData
{
    void UpdateCardWithData();
}