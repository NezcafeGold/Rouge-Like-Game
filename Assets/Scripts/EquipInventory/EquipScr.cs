using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipScr : MonoBehaviour
{
    [SerializeField] private GameObject cellSmallCard;

    private const int AMOUNT_CARDS = 4;
    
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateCardPlaces();
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.UPDATE_EQUIP, UpdateEquip);
    }

    private void UpdateEquip()
    {
        int attack = 0;
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                var scScr = child.GetChild(0).GetComponent<SmallCardInvScr>();
                if (scScr.WeaponData != null)
                {
                    attack += scScr.WeaponData.AttackWeapon;
                }
            }
        }
        PlayerSetup.GetPlayerSetup().UpdateAttack(attack);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GenerateCardPlaces()
    {
        for (int i = 0; i < AMOUNT_CARDS; i++)
        {
            var cell = Instantiate(cellSmallCard, transform, false);
            cell.GetComponent<CellSmallCardScr>().IsEquip = true;
        }
    }
}