using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Animator playerAnimator;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_ATTACK_TURN, Shoot);
    }
    
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_ATTACK_TURN, Shoot);
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        playerAnimator.SetFloat("attack", 1);
    }

    public void IdleAfterShoot()
    {
        playerAnimator.SetFloat("attack", 0);
    }
    
    public void Idle()
    {
        playerAnimator.SetFloat("attack", 0);
    }

    public void DealDamageToEnemy()
    {
        BattleController.Instance.DealDamageToEnemy();
    }
}