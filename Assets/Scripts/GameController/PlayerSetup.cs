using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
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
    [SerializeField] public int Dodge;
    [SerializeField] public GameObject scroll;

    private int defaultAttack;

    private int defaultDice;
    private int defaultDodge;

    private void Start()
    {
        defaultAttack = Attack;
        defaultDice = DiceCount;
        defaultDodge = 0;
    }

    public void SetAttack(int attack)
    {
        Attack = defaultAttack + attack;
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void SetAttackFromChosenWeapon()
    {
        int id = scroll.GetComponent<SimpleScrollSnap>().TargetPanel;
        if (scroll.GetComponent<SimpleScrollSnap>().Panels != null)
            SetAttack(scroll.GetComponent<SimpleScrollSnap>().Panels[id].GetComponent<SmallCardInvScr>()
                .WeaponData.AttackWeapon);
    }

    public void ChangeValue(int value, AbilitityWhatEnum whatEnum)
    {
        switch (whatEnum)
        {
            case AbilitityWhatEnum.ATTACK:
                Attack += value;
                break;
            case AbilitityWhatEnum.HEALTH:
                CurrentLive += value;
                if (CurrentLive > TotalLives)
                    CurrentLive = TotalLives;
                if (CurrentLive < 0)
                    CurrentLive = 0;
                break;
            case AbilitityWhatEnum.DODGE:
                defaultDodge += value;
                break;
            case AbilitityWhatEnum.DICE:
                DiceCount += value;
                break;
        }
        if (CurrentLive <= 0)
        {
            BattleController.Instance.EndOfBattle();
        }
        Messenger.Broadcast(GameEvent.UPDATE_STATS);
    }

    public void DefaultAttack()
    {
        Attack = defaultAttack;
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

    public void DefaultValues()
    {
        Attack = defaultAttack;
        Dodge = 0;
    }
}