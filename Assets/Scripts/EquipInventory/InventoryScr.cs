using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InventoryScr : MonoBehaviour
{
    [SerializeField] private GameObject cellSmallCard;
    [SerializeField] private GameObject smallCard;
    private const int AMOUNT_CARDS = 15;
    private List<GameObject> cellSmallCardsList;

    private void Start()
    {
        gameObject.SetActive(false);
        GenerateCardPlaces();
    }

    private void Awake()
    {
        Messenger.AddListener<WeaponData>(GameEvent.ADD_ITEM_TO_INVENTORY, AddItemToInventory);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<WeaponData>(GameEvent.ADD_ITEM_TO_INVENTORY, AddItemToInventory);
    }


    private void GenerateCardPlaces()
    {
        cellSmallCardsList = new List<GameObject>();
        for (int i = 0; i < AMOUNT_CARDS; i++)
        {
            cellSmallCardsList.Add(Instantiate(cellSmallCard, transform, false));
        }
    }

    private void AddItemToInventory(WeaponData weaponData)
    {
        for (int i = 0; i < cellSmallCardsList.Count; i++)
        {
            var cellSmallCardScr = cellSmallCardsList[i].GetComponent<CellSmallCardScr>();
            if (!cellSmallCardScr.HasItem)
            {
                var smallCardObj = Instantiate(smallCard, cellSmallCardsList[i].transform);
                smallCardObj.GetComponent<SmallCardInvScr>().AddWeaponData(weaponData);
                break;
            }
        }
    }
}