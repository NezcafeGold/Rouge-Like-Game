using System.Collections;
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

    public UnityEvent RollDiceForEnemy;

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
        if (CURRENT_TURN == TURNS.Length - 1)
            CURRENT_TURN = PLAYER_SETUP_TURN;
        else
            CURRENT_TURN++;

        switch (TURNS[CURRENT_TURN])
        {
            case PLAYER_SETUP_TURN:
            {
                Messenger.Broadcast(GameEvent.PLAYER_SETUP_TURN);
                Debug.Log("PLAYER_SETUP_TURN");
                break;
            }
            case ENEMY_SETUP_TURN:
                RollDiceForEnemy.Invoke();
                break;

            case BEFORE_FIGHT:
                Messenger.Broadcast(GameEvent.BEFORE_FIGHT);
                break;

//            case PLAYER_ATTACK_TURN:
//                Messenger.Broadcast(GameEvent.PLAYER_ATTACK_TURN);
//                break;
//            case ENEMY_ATTACK_TURN:
//                Messenger.Broadcast(GameEvent.ENEMY_ATTACK_TURN);
//                break;
        }
    }
}