using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void NextTurn()
    {
        BattleController.Instance.NextTurn();
        gameObject.SetActive(false);
    }
}