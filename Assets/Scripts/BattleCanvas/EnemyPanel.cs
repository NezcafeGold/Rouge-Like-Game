using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EnemyPanel : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemyCard);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemyCard);
    }

    private void AddEnemyCard(GameObject go)
    {
        go = Instantiate(go, transform, false);
        go.GetComponent<RectTransform>().localScale = Vector3.one;
        go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
    }
}