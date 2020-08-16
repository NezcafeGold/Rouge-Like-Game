using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationToBattle : MonoBehaviour
{
    [SerializeField] public GameObject BattleCanvas;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BATTLE_START, StartAnimation);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BATTLE_START, StartAnimation);
    }

    public void LoadBattle()
    {
        BattleCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartAnimation()
    {
        GetComponent<Canvas>().sortingOrder = 8;
        GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
    }
}