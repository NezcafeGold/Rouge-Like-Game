using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEditor.Animations;
using UnityEngine;

public class AbilityController : Singleton<AbilityController>
{
    [SerializeField] private GameObject PlayerAbilityPanel;
    [SerializeField] private GameObject EnemyAbilityPanel;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BEFORE_FIGHT, HandleBeforeFightTurn);
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_TURN, HandlePlayerAttackTurn);
        Messenger.AddListener(GameEvent.ENEMY_ATTACK_TURN, HandleEnemyAttackTurn);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BEFORE_FIGHT, HandleBeforeFightTurn);
        Messenger.RemoveListener(GameEvent.PLAYER_ATTACK_TURN, HandlePlayerAttackTurn);
        Messenger.RemoveListener(GameEvent.ENEMY_SETUP_TURN, HandleEnemyAttackTurn);
    }

    public void HandleBeforeFightTurn()
    {
        foreach (Transform t in PlayerAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.BEFORE_FIGHT)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }

        foreach (Transform t in EnemyAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.BEFORE_FIGHT)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }
    }

    private void HandlePlayerAttackTurn()
    {
        foreach (Transform t in PlayerAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.IN_ATTACK)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }

        foreach (Transform t in EnemyAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.IN_DEFEND)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }
    }

    private void HandleEnemyAttackTurn()
    {
        foreach (Transform t in EnemyAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.IN_ATTACK)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }

        foreach (Transform t in PlayerAbilityPanel.transform)
        {
            if (t.gameObject.GetComponent<Ability>().abilityData.AbilityWhenEnum == AbilityWhenEnum.IN_DEFEND)
            {
                HandleAbility(t.gameObject.GetComponent<Ability>());
            }
        }
    }

    private void HandleAbility(Ability ability)
    {
        if (ability.ValueAb > 0)
        {
            ability.Activate();
            if (ability.abilityData.AbilityDoEnum == AbilityDoEnum.TO_MYSELF)
            {
                if (ability.side == Side.PLAYER)
                {
                    PlayerSetup.Instance.ChangeValue(ability.ValueAb, ability.abilityData.abilityWhatEnum);
                }
                else if (ability.side == Side.ENEMY)
                {
                    EnemySetup.Instance.ChangeValue(ability.ValueAb, ability.abilityData.abilityWhatEnum);
                }
            }
            else if (ability.abilityData.AbilityDoEnum == AbilityDoEnum.TO_ENEMY)
            {
                if (ability.side == Side.PLAYER)
                {
                    EnemySetup.Instance.ChangeValue(-ability.ValueAb, ability.abilityData.abilityWhatEnum);
                }
                else if (ability.side == Side.ENEMY)
                {
                    PlayerSetup.Instance.ChangeValue(-ability.ValueAb, ability.abilityData.abilityWhatEnum);
                }
            }
        }
    }
}