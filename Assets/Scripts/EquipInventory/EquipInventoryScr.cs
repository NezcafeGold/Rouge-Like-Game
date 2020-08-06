using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryScr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowInventory()
    {
        int order = gameObject.transform.GetComponent<Canvas>().sortingOrder;
        if (order == 1)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 0;
        else if (order == 0)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 1;
        Messenger.Broadcast(GameEvent.SHOW_INVENTORY);
    }
    
    public void ShowSpellBook()
    {
        int order = gameObject.transform.GetComponent<Canvas>().sortingOrder;
        if (order == 1)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 0;
        else if (order == 0)
            gameObject.transform.GetComponent<Canvas>().sortingOrder = 1;
        Messenger.Broadcast(GameEvent.SHOW_SPELL_BOOK);
    }
}