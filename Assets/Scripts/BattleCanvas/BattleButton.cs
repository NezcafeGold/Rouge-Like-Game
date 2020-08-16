using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{


    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SHOW_BATTLE_BUTTON, ShowBattleButton);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SHOW_BATTLE_BUTTON, ShowBattleButton);
    }

    private void ShowBattleButton()
    {
        gameObject.SetActive(true);
    }


}