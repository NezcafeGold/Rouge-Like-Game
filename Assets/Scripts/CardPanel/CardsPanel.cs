using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Awake()
    {
        Messenger.AddListener<GridCell>(GameEvent.PLAYER_MOVE_ON_CELL, ShowCardsFromTile);
    }

    //Показ карт на панели, информация берется из gridCell. Если тип бо
    private void ShowCardsFromTile(GridCell gridCell)
    {
        TileCard tileCard = gridCell.TileCard;
        foreach (StructCardTypeAmount sctruct in tileCard.cardList)
        {
            int amount = sctruct.amount;
            for (int i = 0; i < amount; i++)
            {
                var cardObj = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                
                  cardObj.GetComponent<CardScr>().SetType(sctruct.cardType);
//                if (cardObj.GetComponent<Image>().sprite == null)
//                {
//                    cardObj.GetComponent<Image>().sprite = sctruct.cardData.CardShirt;
//                }
                cardObj.transform.SetParent(gameObject.transform, false);
            }
        }

        gameObject.SetActive(true);
    }

    public void ShuffleCards()
    {
        StartCoroutine(ChangeSpacingAndRotate());
    }

    private IEnumerator ChangeSpacingAndRotate()
    {
        var spacing = gameObject.GetComponent<GridLayoutGroup>().spacing.x;
        var widht = gameObject.GetComponent<GridLayoutGroup>().cellSize.x;

        //TODO: VECTOR3.LERP
        Vector2 defaultSpacing = new Vector2(spacing, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
        Vector2 endingSpacing = new Vector2(-widht, gameObject.GetComponent<GridLayoutGroup>().spacing.y);


        while (spacing > -widht + 0.5f)
        {
            spacing = Mathf.Lerp(defaultSpacing.x + 200, endingSpacing.x, Time.time / 2f);
            gameObject.GetComponent<GridLayoutGroup>().spacing =
                new Vector3(spacing, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
            yield return null;
        }

        float angle = 0;
        while (angle < 90f)
        {
            angle = Mathf.LerpAngle(angle, 91f, Time.time / 30);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        Messenger.Broadcast(GameEvent.CHANGE_CARD_SHIRT);
        List<Transform> list = transform.Cast<Transform>().ToList();
        foreach (var tran in list)
        {
            tran.SetParent(null);
        }

        while (list.Count>0)
        {
            var tl = list[Random.Range(0, list.Count)];
            tl.SetParent(transform);
            list.Remove(tl);
        }
        

        angle = transform.eulerAngles.y;
        while (angle > 0f)
        {
            angle = Mathf.LerpAngle(angle, -1f, Time.time / 30);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        var spacingCurrent = gameObject.GetComponent<GridLayoutGroup>().spacing.x;
        while (spacingCurrent < defaultSpacing.x)
        {
            spacingCurrent = Mathf.Lerp(spacingCurrent, defaultSpacing.x + 1f, Time.time / 120);
            gameObject.GetComponent<GridLayoutGroup>().spacing =
                new Vector3(spacingCurrent, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
            yield return null;
        }
    }
}