﻿using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BattleController : Singleton<BattleController>
{
    public const int PLAYER_SETUP_TURN = 0;
    public const int ENEMY_SETUP_TURN = 1;
    public const int BEFORE_FIGHT = 2;
    public const int PLAYER_ATTACK_TURN = 3;
    public const int ENEMY_ATTACK_TURN = 4;

    public UnityEvent RollDiceForEnemy, BeforeFight;

    public int CURRENT_TURN;


    public int[] TURNS =
    {
        PLAYER_SETUP_TURN, ENEMY_SETUP_TURN, BEFORE_FIGHT, PLAYER_ATTACK_TURN, ENEMY_ATTACK_TURN
    };

    //Атака: 

    private void Start()
    {
        CURRENT_TURN = PLAYER_SETUP_TURN;
    }

    public void NextTurn()
    {
        if (CURRENT_TURN == TURNS.Length)
            CURRENT_TURN = PLAYER_SETUP_TURN;
        else
            CURRENT_TURN++;

        HandleCurrentTurn();
    }

    public void HandleCurrentTurn()
    {
//        switch (TURNS[CURRENT_TURN])
//        {
//            case PLAYER_SETUP_TURN:
//            {
//                Messenger.Broadcast(GameEvent.PLAYER_SETUP_TURN);
//                Debug.Log("PLAYER_SETUP_TURN");
//                break;
//            }
//            case ENEMY_SETUP_TURN:
//                Messenger.Broadcast(GameEvent.ENEMY_SETUP_TURN);
//                break;
//
//            case BEFORE_FIGHT:
//                Messenger.Broadcast(GameEvent.BEFORE_FIGHT);
//                break;
//
//            case PLAYER_ATTACK_TURN:
//                Messenger.Broadcast(GameEvent.PLAYER_ATTACK_TURN);
//                break;
//
////            case ENEMY_ATTACK_TURN:
////                Messenger.Broadcast(GameEvent.ENEMY_ATTACK_TURN);
////                break;
//        }


        if (TURNS[CURRENT_TURN] == PLAYER_SETUP_TURN)
        {
            Messenger.Broadcast(GameEvent.PLAYER_SETUP_TURN);
            Debug.Log("PLAYER_SETUP_TURN");
        }
        else if (TURNS[CURRENT_TURN] == ENEMY_SETUP_TURN)
        {
            Messenger.Broadcast(GameEvent.ENEMY_SETUP_TURN);
            Debug.Log("ENEMY_SETUP_TURN");
            StartCoroutine(WaitForNextTurn(2f));
        }
        else if (TURNS[CURRENT_TURN] == BEFORE_FIGHT)
        {
            Messenger.Broadcast(GameEvent.BEFORE_FIGHT);
            Debug.Log("BEFORE_FIGHT");
            StartCoroutine(WaitForNextTurn(2f));
        }
        else if (TURNS[CURRENT_TURN] == PLAYER_ATTACK_TURN)
        {
            Messenger.Broadcast(GameEvent.PLAYER_ATTACK_TURN);
            Debug.Log("PLAYER_ATTACK_TURN");
            StartCoroutine(WaitForNextTurn(3.5f));
        }
        else if (TURNS[CURRENT_TURN] == ENEMY_ATTACK_TURN)
        {
            Messenger.Broadcast(GameEvent.ENEMY_ATTACK_TURN);
            Debug.Log("ENEMY_ATTACK_TURN");
        }
    }

    private IEnumerator WaitForNextTurn(float time)
    {
        yield return new WaitForSeconds(time);
        NextTurn();
    }

    public void DealDamageToEnemy()
    {
        EnemySetup.Instance.ChangeValue(-PlayerSetup.Instance.Attack, AbilitityWhatEnum.HEALTH);
        Messenger.Broadcast(GameEvent.ENEMY_ANIM_DEFEND);
    }
}