using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class CardsPanel : MonoBehaviour
{
    [SerializeField] private GameObject card;
    private List<GameObject> cardsList;

    private void Start()
    {
        gameObject.SetActive(false);
        cardsList = new List<GameObject>();
    }

    private void Awake()
    {
        Messenger.AddListener<TileCell>(GameEvent.PLAYER_MOVE_ON_CELL, ShowCardsFromTile);
        Messenger.AddListener(GameEvent.CLOSE_CARDS_PANEL, CloseCardsPanel);
    }

    //Показ карт на панели, информация берется из gridCell. Если тип бо
    private void ShowCardsFromTile(TileCell tileCell)
    {
        if (!tileCell.TileСellValues.IsVisited)
        {
            TypeTileCardData typeTileCardData = tileCell.TileСellValues.TypeTileCardData;
            foreach (StructCardTypeAmount sctruct in typeTileCardData.cardList)
            {
                int amount = sctruct.amount;
                for (int i = 0; i < amount; i++)
                {
                    var cardObj = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);

                    cardObj.GetComponent<CardScr>().SetType(sctruct.cardType);
                    cardObj.transform.SetParent(gameObject.transform, false);
                    cardsList.Add(cardObj);
                }
            }

            gameObject.SetActive(true);
            Messenger.Broadcast(GameEvent.SHUFFLE_BUTTON);
            tileCell.TileСellValues.IsVisited = true;
        }
    }

    private void StartAnimation()
    {
        Messenger.Broadcast(GameEvent.BLOCK_TO_ROTATE);
        GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
    }

    private void EndAnimation()
    {
        Messenger.Broadcast(GameEvent.ALLOW_TO_ROTATE);
    }
    
    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void CloseCardsAndShuffle()
    {
        Messenger.Broadcast(GameEvent.CHANGE_CARD_SHIRT);
        List<Transform> list = transform.Cast<Transform>().ToList();
        foreach (var tran in list)
        {
            tran.SetParent(null);
        }

        while (list.Count > 0)
        {
            var tl = list[Random.Range(0, list.Count)];
            tl.SetParent(transform);
            list.Remove(tl);
        }
    }


    private void CloseCardsPanel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
    }
}