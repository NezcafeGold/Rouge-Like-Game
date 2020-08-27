using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValuesChangingAnimation : MonoBehaviour
{
    [SerializeField] AbilitityWhatEnum whatEnum;
    private Animator animator;
    private Text text;

    private void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
        text.enabled = false;
    }

    private void Awake()
    {
        Messenger.AddListener<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, HandleAnimPlayerValue);
        Messenger.AddListener<AbilitityWhatEnum, int>(GameEvent.ANIM_ENEMY_VALUE, HandleAnimEnemyValue);
    }



    private void OnDestroy()
    {
        Messenger.RemoveListener<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, HandleAnimPlayerValue);
        Messenger.RemoveListener<AbilitityWhatEnum, int>(GameEvent.ANIM_ENEMY_VALUE, HandleAnimEnemyValue);
    }

    private void HandleAnimPlayerValue(AbilitityWhatEnum whatEnum, int value)
    {
        if (transform.parent.name == "PlayerSide" && this.whatEnum == whatEnum)
        {
            if (value < 0)
                text.color = Color.red;
            else text.color = Color.green;
            text.text = (value < 0 ? "" : "+") + value.ToString();
            animator.SetTrigger("playPlayer");
        }
    }
    
    private void HandleAnimEnemyValue(AbilitityWhatEnum whatEnum, int value)
    {
        if (transform.parent.name == "EnemySide" && this.whatEnum == whatEnum)
        {
            if (value < 0)
                text.color = Color.red;
            else text.color = Color.green;
            text.text = (value < 0 ? "" : "+") + value.ToString();
            animator.SetTrigger("playEnemy");
        }
    }
}