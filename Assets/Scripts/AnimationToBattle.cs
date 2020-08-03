using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationToBattle : MonoBehaviour
{
    [SerializeField] public GameObject BattleCanvas;

    private void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.BEGIN_BATTLE, StartAnimation);
    }

    public void LoadBattle()
    {
       BattleCanvas.SetActive(true);
       gameObject.SetActive(false);
    }

    public void StartAnimation(GameObject ed)
    {
        GetComponent<Canvas>().sortingOrder = 3;
        GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
    }
}
