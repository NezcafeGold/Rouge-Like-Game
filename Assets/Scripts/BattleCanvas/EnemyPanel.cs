using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EnemyPanel : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemyCard);
        Messenger.AddListener(GameEvent.ENEMY_ANIM_DEFEND, AnimDefend);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemyCard);
        Messenger.RemoveListener(GameEvent.ENEMY_ANIM_DEFEND, AnimDefend);
    }

    private void AddEnemyCard(GameObject go)
    {
        go = Instantiate(go, transform, false);
        go.GetComponent<RectTransform>().localScale = Vector3.one;
        go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
    }
    
    private void AnimDefend()
    {
        anim.SetTrigger("GetDamage");
    }
}