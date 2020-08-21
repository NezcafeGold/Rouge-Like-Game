using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtPlayerNum : MonoBehaviour
{
    private Transform extAttack;
    private Transform extHP;
    private Transform extDodge;
    private Transform extDice;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.UPDATE_STATS, UpdateExtPlayerNum);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.UPDATE_STATS, UpdateExtPlayerNum);
    }

    private void Start()
    {
        extAttack = transform.Find("ExtAttack");
        extHP = transform.Find("ExtHP");
        extDodge = transform.Find("ExtDodge");
        extDice = transform.Find("ExtDice");

        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    private void UpdateExtPlayerNum()
    {
        PlayerSetup playerSetup = PlayerSetup.Instance;
        if (playerSetup.ExtAttack > 0)
            extAttack.gameObject.SetActive(true);
        else
            extAttack.gameObject.SetActive(false);

        if (playerSetup.ExtHp > 0)
            extHP.gameObject.SetActive(true);
        else
            extHP.gameObject.SetActive(false);

        if (playerSetup.ExtDodge > 0)
            extDodge.gameObject.SetActive(true);
        else
            extDodge.gameObject.SetActive(false);

        if (playerSetup.ExtDiceDecrease > 0)
            extDice.gameObject.SetActive(true);
        else
            extDice.gameObject.SetActive(false);

        try
        {
            extAttack.Find("ExtAttackNum").GetComponent<TextMeshProUGUI>().text =
                " + " + playerSetup.ExtAttack;
            extHP.Find("ExtHPNum").GetComponent<TextMeshProUGUI>().text =
                " + " + playerSetup.ExtHp;
            extDodge.Find("ExtDodgeNum").GetComponent<TextMeshProUGUI>().text =
                " + " + playerSetup.ExtDodge + "%";
            extDice.Find("ExtDiceNum").GetComponent<TextMeshProUGUI>().text =
                "" + playerSetup.ExtDiceDecrease;
        }
        catch (Exception e)
        {
            Debug.Log("Error at ExtPlayerNum " + e);
        }
    }
}