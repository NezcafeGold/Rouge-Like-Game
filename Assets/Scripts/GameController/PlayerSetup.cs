using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] [Tooltip("Не более 5")]
    public List<AbilityData> Abilities;

   
    [SerializeField] public int Attack;
    [SerializeField] public int CurrentLive;
    [SerializeField] public int TotalLives;
    [SerializeField] public int DiceCount;

    private int defaultAttack;

    private void Start()
    {
        defaultAttack = Attack;
    }

    public static PlayerSetup GetPlayerSetup()
    {
        return GameObject.Find("PlayerSetup").GetComponent<PlayerSetup>();
    }

    public void UpdateAttack(int attack)
    {
        Attack = defaultAttack + attack;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }
}