using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Messenger.AddListener(GameEvent.BATTLE_START, AddCardsToBattlePanel);
    }

    private void AddCardsToBattlePanel()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (Transform t in transform)
        {
            GameObject card = null;
            try
            {
                card = t.GetChild(0).gameObject;
            }
            catch (Exception e)
            {
                continue;
            }
            if (card != null)
            {
                list.Add(card);
            }
        }

        Messenger.Broadcast<List<GameObject>>(GameEvent.ADD_ITEMS_TO_BATTLE, list);
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