using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    
    

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_TURN, HandleAttackTurn);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_ATTACK_TURN, HandleAttackTurn);
    }
    
    private void HandleAttackTurn()
    {
        Transform abPan = transform.Find("AbilityPanel");
        foreach (Transform ab in abPan)
        {
            ab.GetComponent<Ability>().HandleAbility();
        }
    }
}
