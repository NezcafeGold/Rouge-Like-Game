using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Singleton;
using UnityEngine;

public class PlayerSetup : Singleton<PlayerSetup>
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
    private int extAttack;
    private int extHP;
    private int extDodge;
    private int extDiceDecrease;

    public int ExtAttack
    {
        get { return extAttack; }
    }

    public int ExtHp
    {
        get { return extHP; }
    }

    public int ExtDodge
    {
        get { return extDodge; }
    }

    public int ExtDiceDecrease
    {
        get { return extDiceDecrease; }
    }

    private void Start()
    {
        defaultAttack = Attack;
    }

    public void AddAttack(int attack)
    {
        Attack = defaultAttack + attack;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }


    public void AddExtAttack(int attack)
    {
        extAttack = attack;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void AddExtHealth(int hp)
    {
        extHP = hp;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void AddExtDodge(int dodge)
    {
        extDodge = dodge;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void AddExtDiceDecrease(int diceDecrease)
    {
        extDiceDecrease = diceDecrease;
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