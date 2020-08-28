using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Animator playerAnimator;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_TURN, Shoot);
        Messenger.AddListener(GameEvent.PLAYER_ANIM_DEFEND, HandleAnimDefend);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_ATTACK_TURN, Shoot);
        Messenger.RemoveListener(GameEvent.PLAYER_ANIM_DEFEND, HandleAnimDefend);
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        playerAnimator.SetTrigger("Attack");
    }


    public void DealDamageToEnemy()
    {
        BattleController.Instance.DealDamageToEnemy();
    }
    
    private void HandleAnimDefend()
    {
        playerAnimator.SetTrigger("GetDamage");
    }
    
}