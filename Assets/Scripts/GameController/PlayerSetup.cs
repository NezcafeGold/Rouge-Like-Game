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
    [SerializeField] public int CurrentStaminaPoints;
    [SerializeField] public int TotalStaminaPoints;

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

    public void SubtractStamina(int value)
    {
        CurrentStaminaPoints -= value;
        if (CurrentStaminaPoints < 0) CurrentStaminaPoints = 0;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void AddStamina(int value)
    {
        CurrentStaminaPoints += value;
        if (CurrentStaminaPoints > TotalStaminaPoints) CurrentStaminaPoints = TotalStaminaPoints;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }
}