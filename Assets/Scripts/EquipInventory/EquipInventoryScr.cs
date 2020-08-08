using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryScr : MonoBehaviour
{
    private GameObject inv;
    private GameObject sb;

    private void Start()
    {
        inv = transform.Find("Inventory").gameObject;
        sb = transform.Find("SpellBook").gameObject;
    }


    public void ShowInventory()
    {
        int order = gameObject.transform.GetComponent<Canvas>().sortingOrder;
        if (order == 1)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 0;
        else if (order == 0)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 1;

        if (!inv.activeSelf)
        {
            inv.SetActive(true);
            if (sb.activeSelf)
                ShowSpellBook();
        }
        else inv.SetActive(false);
    }

    public void ShowSpellBook()
    {
        int order = gameObject.transform.GetComponent<Canvas>().sortingOrder;
        if (order == 1)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 0;
        else if (order == 0)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 1;

        if (!sb.activeSelf)
        {
            sb.SetActive(true);
            if (inv.activeSelf)
                ShowInventory();
        }
        else sb.SetActive(false);
    }
}