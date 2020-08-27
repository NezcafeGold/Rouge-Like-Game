using Singleton;
using UnityEngine;

public class EnemySetup : Singleton<EnemySetup>
{
    public int Attack;
    public int CurrentLive;
    public int DiceCount;
    public int Dodge;

    private void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, CastSetupFromCard);
    }
    
    public void CastSetupFromCard(GameObject cardGO)
    {
        Attack = cardGO.GetComponent<CardScr>().EnemyData.AttackEnemy;
        CurrentLive = cardGO.GetComponent<CardScr>().EnemyData.HpEnemy;
        DiceCount = cardGO.GetComponent<CardScr>().EnemyData.DiceAmount;
        Messenger.Broadcast(GameEvent.UPDATE_ENEMY_STATS);
    }
    
    public void ChangeValueFromAbility(int value, AbilitityWhatEnum whatEnum)
    {
        switch (whatEnum)
        {
            case AbilitityWhatEnum.ATTACK:
                Attack += value;
                break;
            case AbilitityWhatEnum.HEALTH:
                CurrentLive += value;
                if (CurrentLive < 0)
                    CurrentLive = 0;
                break;
            case AbilitityWhatEnum.DODGE:
                Dodge += value;
                break;
            case AbilitityWhatEnum.DICE:
                DiceCount += value;
                break;
        }
        Messenger.Broadcast(GameEvent.UPDATE_ENEMY_STATS);
    }
}
