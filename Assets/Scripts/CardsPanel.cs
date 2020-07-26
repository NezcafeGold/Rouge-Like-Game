using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;


public class CardsPanel : MonoBehaviour
{
    [SerializeField] private GameObject card;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowCardsFromTile(TileCard tileCard)
    {
        int amountCard = tileCard.cardList.Count;
        foreach (StructCardTypeAmount sctruct in tileCard.cardList)
        {
            int amount = sctruct.amount;
            for (int i = 0; i < amount; i++)
            {
                var cardObj = Instantiate(card, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                cardObj.GetComponent<Image>().sprite = sctruct.card.CardShirt;
                cardObj.transform.SetParent(gameObject.transform, false);
            }
        }

        gameObject.SetActive(true);
    }
}