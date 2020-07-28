using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardScr : MonoBehaviour
{
    [SerializeField] private Sprite shirtOfCard;
    private Sprite faceOfCard;
    private bool isFace = true;
    
    private bool isRotatable = true;
    
    private CardType cardType;
    private EnemyData enemyData;


    public Sprite FaceOfCard
    {
        get { return faceOfCard; }
        set
        {
            faceOfCard = value;
            gameObject.GetComponent<Image>().sprite = faceOfCard;
        }
    }

    public EnemyData EnemyData
    {
        get { return enemyData; }
        set
        {
            enemyData = value;
            shirtOfCard = enemyData.SpriteEnemy;
        }
    }

    public Sprite ShirtOfCard
    {
        get { return shirtOfCard; }
        set
        {
            shirtOfCard = value;
            gameObject.GetComponent<Image>().sprite = shirtOfCard;
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.CHANGE_CARD_SHIRT, ChangeCardShirt);
        Messenger.AddListener(GameEvent.BLOCK_TO_ROTATE, BlockToRotate);
    }

    private void Start()
    {
        
    }

    private void BlockToRotate()
    {
        isRotatable = false;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CHANGE_CARD_SHIRT, ChangeCardShirt);
        Messenger.RemoveListener(GameEvent.BLOCK_TO_ROTATE, BlockToRotate);
    }

    private void ChangeCardShirt()
    {
        transform.GetComponent<Image>().sprite = shirtOfCard;
    }

    public void RotateAndShowFace()
    {
        if (isRotatable)
            StartCoroutine(Rotate());
        Messenger.Broadcast(GameEvent.BLOCK_TO_ROTATE);
        
    }

    private IEnumerator Rotate()
    {
        float angle = 0;
        while (angle < 90f)
        {
            angle = Mathf.LerpAngle(angle, 91f, Time.time / 30);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        gameObject.transform.GetComponent<Image>().sprite = faceOfCard;
        
        angle = transform.eulerAngles.y;
        while (angle > 0f)
        {
            angle = Mathf.LerpAngle(angle, -1f, Time.time / 30);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }
    }


    public void SetType(CardType sctructCardType)
    {
        try
        {
            if (sctructCardType == CardType.BATTLE)
            {
                List<EnemyData> enemies = GameObject.Find("LevelController").GetComponent<LevelController>()
                    .CurrentLevel
                    .Enemies;
                enemyData = enemies[Random.Range(0, enemies.Count)];
                FaceOfCard = enemyData.SpriteEnemy;
                isFace = true;
            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
