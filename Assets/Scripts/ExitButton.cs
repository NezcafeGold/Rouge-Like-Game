using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private GameObject cardsPanel;
    void Start()
    {
        cardsPanel = GameObject.Find("CardsPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (cardsPanel.active)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void DisableClick()
    {
        cardsPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
