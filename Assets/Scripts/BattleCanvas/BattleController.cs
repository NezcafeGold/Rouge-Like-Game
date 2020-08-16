using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public const int PLAYER_SETUP_TURN = 0;
    public const int ENEMY_SETUP_TURN = 1;
    public const int PLAYER_ATTACK_TURN = 2;
    public const int ENEMY_ATTACK_TURN = 3;

    public static int CURRENT_TURN;

    public static int[] TURNS = {PLAYER_SETUP_TURN, ENEMY_SETUP_TURN, PLAYER_ATTACK_TURN, ENEMY_ATTACK_TURN};

    private void Start()
    {
        CURRENT_TURN = PLAYER_SETUP_TURN;
    }

    public static void NextTurn()
    {
        if (CURRENT_TURN == TURNS.Length - 1)
            CURRENT_TURN = PLAYER_SETUP_TURN;
        else
            CURRENT_TURN++;

        switch (TURNS[CURRENT_TURN])
        {
            case PLAYER_SETUP_TURN:
                Messenger.Broadcast(GameEvent.PLAYER_SETUP_TURN);
                break;

            case ENEMY_SETUP_TURN:
                Messenger.Broadcast(GameEvent.BATTLE_ENEMY_SETUP_TURN);
                break;

            case PLAYER_ATTACK_TURN:
   
                Messenger.Broadcast(GameEvent.PLAYER_ATTACK_TURN);
                break;

            case ENEMY_ATTACK_TURN:
                Messenger.Broadcast(GameEvent.ENEMY_ATTACK_TURN);
                break;
        }
    }

}