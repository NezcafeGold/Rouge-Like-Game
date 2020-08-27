using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class AbilityController : Singleton<AbilityController>
{
    [SerializeField] private GameObject PlayerAbilityPanel;
    [SerializeField] private GameObject EnemyAbilityPanel;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.BEFORE_FIGHT, HandleBeforeFightTurn);
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_TURN, HandlePlayerAttackTurn);
        Messenger.AddListener(GameEvent.ENEMY_SETUP_TURN, HandleEnemyAttackTurn);
    }

    private void HandleBeforeFightTurn()
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

        if (BattleController.Instance.CURRENT_TURN == 2)
            BattleController.Instance.NextTurn();
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

        if (BattleController.Instance.CURRENT_TURN == 3)
            BattleController.Instance.NextTurn();
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

        if (BattleController.Instance.CURRENT_TURN == 4)
            BattleController.Instance.NextTurn();
    }

    private void HandleAbility(Ability ability)
    {
        if (ability.abilityData.AbilityDoEnum == AbilityDoEnum.TO_MYSELF)
        {
            if (ability.side == Side.PLAYER)
            {
                PlayerSetup.Instance.ChangeValueFromAbility(ability.ValueAb, ability.abilityData.abilityWhatEnum);
            }
            else if (ability.side == Side.ENEMY)
            {
                EnemySetup.Instance.ChangeValueFromAbility(ability.ValueAb, ability.abilityData.abilityWhatEnum);
            }
        }
        else if (ability.abilityData.AbilityDoEnum == AbilityDoEnum.TO_ENEMY)
        {
            if (ability.side == Side.PLAYER)
            {
                EnemySetup.Instance.ChangeValueFromAbility(-ability.ValueAb, ability.abilityData.abilityWhatEnum);
            }
            else if (ability.side == Side.ENEMY)
            {
                PlayerSetup.Instance.ChangeValueFromAbility(-ability.ValueAb, ability.abilityData.abilityWhatEnum);
            }
        }
    }
}