﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static int PLAYER_TURN = 1;
    public static int ENEMY_TURN = 2;

    public static int CURRENT_TURN;
    private void Start()
    {
        CURRENT_TURN = PLAYER_TURN;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SHOW_BATTLE_CONTROLLER, ShowBattleButton);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SHOW_BATTLE_CONTROLLER, ShowBattleButton);
    }

    private void ShowBattleButton()
    {
        gameObject.SetActive(true);
        CURRENT_TURN = ENEMY_TURN;
    }

    public void PlayerAttackEnemy()
    {
    }
}