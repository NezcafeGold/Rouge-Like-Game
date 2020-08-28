using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;
using UnityEngine.UI;

public class ValuesChangingAnimation : MonoBehaviour
{
    [SerializeField] private AbilitityWhatEnum whatEnum;
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

    public void HandleAnimPlayerValue(AbilitityWhatEnum whatEnums, int value)
    {
        if (transform.parent.name == "PlayerSide" && whatEnum == whatEnums)
        {
            if (value < 0)
                text.color = Color.red;
            else text.color = Color.green;
            text.text = (value < 0 ? "" : "+") + value.ToString();
            animator.SetTrigger("playPlayer");
        }
    }
    
    public void HandleAnimEnemyValue(AbilitityWhatEnum whatEnums, int value)
    {
        if (transform.parent.name == "EnemySide" && whatEnum == whatEnums)
        {
            if (value < 0)
                text.color = Color.red;
            else text.color = Color.green;
            text.text = (value < 0 ? "" : "+") + value.ToString();
            animator.SetTrigger("playEnemy");
        }
    }
}