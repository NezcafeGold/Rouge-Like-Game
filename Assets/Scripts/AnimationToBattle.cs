using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationToBattle : MonoBehaviour
{
    [SerializeField] public GameObject BattleCanvas;
    [SerializeField] public List<GameObject> ObjectsOfGameField;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BATTLE_START_ANIM, ToBattleAnimation);
        Messenger.AddListener(GameEvent.END_BATTLE, ToFieldAnimation);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BATTLE_START_ANIM, ToBattleAnimation);
        Messenger.RemoveListener(GameEvent.END_BATTLE, ToFieldAnimation);
    }

    public void ToBattleAnimation()
    {
        GetComponent<Canvas>().sortingOrder = 8;
        anim.SetTrigger("BattleStart");
    }

    private void LoadBattle()
    {
        BattleCanvas.SetActive(true);
        foreach (GameObject obj in ObjectsOfGameField)
        {
            obj.SetActive(false);
        }

        Messenger.Broadcast(GameEvent.BATTLE_START);
    }

    private void EndLoadBattle()
    {
        GetComponent<Canvas>().sortingOrder = -1;
    }

    public void ToFieldAnimation()
    {
        GetComponent<Canvas>().sortingOrder = 8;
        anim.SetTrigger("BattleEnd");
    }

    public void LoadField()
    {
        foreach (GameObject obj in ObjectsOfGameField)
        {
            obj.SetActive(true);
        }

        BattleCanvas.SetActive(false);
        Messenger.Broadcast(GameEvent.END_BATTLE_DESTROY);
        PlayerSetup.Instance.DefaultAttack();
    }

    public void EndLoadField()
    {
        GetComponent<Canvas>().sortingOrder = -1;
    }
}