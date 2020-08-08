using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanel : MonoBehaviour
{
    void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.BEGIN_BATTLE, AddEnemyCard);
    }

    private void AddEnemyCard(GameObject go)
    {
        go = Instantiate(go, transform, false);
        go.GetComponent<RectTransform>().localScale = Vector3.one;
        //go.transform.position = new Vector3(0, 0, 0);
        go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
    }
}