using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SmallCardInvScr : MonoBehaviour
{
    //private bool hasItem;
    private WeaponData weaponData;
    private Sprite standartSprite;

//    public bool HasItem
//    {
//        get { return hasItem; }
//        set { hasItem = value; }
//    }

    public WeaponData WeaponData
    {
        get { return weaponData; }
        set { weaponData = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        standartSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void AddWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        gameObject.transform.parent.GetComponent<CellSmallCardScr>().HasItem = true;
        UpdateVisualCard();
    }

    public void UpdateVisualCard()
    {
        if (weaponData != null)
        {
            gameObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = weaponData.name;
            gameObject.transform.Find("RightAngle").GetComponent<TextMeshProUGUI>().text =
                weaponData.AttackWeapon.ToString();
            gameObject.GetComponent<Image>().sprite = weaponData.SpriteWeapon;
        }
    }
}