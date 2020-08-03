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
        Messenger.AddListener<GridCell>(GameEvent.PLAYER_MOVE_ON_CELL, ShowCardsFromTile);
        Messenger.AddListener(GameEvent.CLOSE_CARDS_PANEL, CloseCardsPanel);
    }

    //Показ карт на панели, информация берется из gridCell. Если тип бо
    private void ShowCardsFromTile(GridCell gridCell)
    {
        if (!gridCell.Visited)
        {
            TileCard tileCard = gridCell.TileCard;
            foreach (StructCardTypeAmount sctruct in tileCard.cardList)
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
            gridCell.Visited = true;
        }
    }

    public void ShuffleCards()
    {
        StartCoroutine(ChangeSpacingAndRotate());
    }

    private IEnumerator ChangeSpacingAndRotate()
    {
        var spacing = gameObject.GetComponent<GridLayoutGroup>().spacing.x;
        var widht = gameObject.GetComponent<GridLayoutGroup>().cellSize.x;

        Vector2 defaultSpacing = new Vector2(spacing, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
        Vector2 endingSpacing = new Vector2(-widht, gameObject.GetComponent<GridLayoutGroup>().spacing.y);

        while (spacing > -widht + 0.5f)
        {
            spacing = Mathf.Lerp(defaultSpacing.x, endingSpacing.x, Time.fixedTime / 3f);
            gameObject.GetComponent<GridLayoutGroup>().spacing =
                new Vector3(spacing, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
            yield return null;
        }


        float angle = 0;
        while (angle < 90f)
        {
            angle = Mathf.LerpAngle(angle, 91f, Time.fixedTime / 200);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        var euler = transform.eulerAngles;
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
            tl.transform.eulerAngles = euler;
            list.Remove(tl);
        }


        angle = transform.eulerAngles.y;
        while (angle > 0f)
        {
            angle = Mathf.LerpAngle(angle, -1f, Time.fixedTime / 200);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        var spacingCurrent = gameObject.GetComponent<GridLayoutGroup>().spacing.x;
        while (spacingCurrent < defaultSpacing.x)
        {
            spacingCurrent = Mathf.Lerp(spacingCurrent, defaultSpacing.x + 1f, Time.fixedTime / 40f);
            gameObject.GetComponent<GridLayoutGroup>().spacing =
                new Vector3(spacingCurrent, gameObject.GetComponent<GridLayoutGroup>().spacing.y);
            yield return null;
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