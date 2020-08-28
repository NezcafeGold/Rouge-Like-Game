using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BattleController : Singleton<BattleController>
{
    [HideInInspector] public const int PLAYER_SETUP_TURN = 0;
    [HideInInspector] public const int ENEMY_SETUP_TURN = 1;
    [HideInInspector] public const int BEFORE_FIGHT = 2;
    [HideInInspector] public const int PLAYER_ATTACK_TURN = 3;
    [HideInInspector] public const int ENEMY_ATTACK_TURN = 4;
    [HideInInspector] public const int END_BATTLE = 5;
    [HideInInspector] public int CURRENT_TURN;

    [HideInInspector] public int[] TURNS =
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
        if (CURRENT_TURN == END_BATTLE)
        {
            Messenger.Broadcast(GameEvent.END_BATTLE);
            return;
        }

        if (CURRENT_TURN == ENEMY_ATTACK_TURN)
            CURRENT_TURN = PLAYER_SETUP_TURN;
        else
            CURRENT_TURN++;

        HandleCurrentTurn();
    }

    public void HandleCurrentTurn()
    {
        if (TURNS[CURRENT_TURN] == PLAYER_SETUP_TURN)
        {
            PlayerSetup.Instance.DefaultValues();
            Messenger.Broadcast(GameEvent.PLAYER_SETUP_TURN);
            Debug.Log("PLAYER_SETUP_TURN");
        }
        else if (TURNS[CURRENT_TURN] == ENEMY_SETUP_TURN)
        {
            Messenger.Broadcast(GameEvent.ENEMY_SETUP_TURN);
            Debug.Log("ENEMY_SETUP_TURN");
            StartCoroutine(WaitForNextTurn(2.5f));
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
            StartCoroutine(WaitForNextTurn(3.5f));
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

    public void DealDamageToPlayer()
    {
        PlayerSetup.Instance.ChangeValue(-EnemySetup.Instance.Attack, AbilitityWhatEnum.HEALTH);
        Messenger.Broadcast(GameEvent.PLAYER_ANIM_DEFEND);
    }

    public void EndOfBattle()
    {
        CURRENT_TURN = END_BATTLE;
    }
}