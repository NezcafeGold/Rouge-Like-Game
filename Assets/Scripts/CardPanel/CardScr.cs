﻿using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardScr : MonoBehaviour
{
    [SerializeField] private Sprite shirtOfCard;

    [SerializeField] private GameObject faceCard;
    [SerializeField] private GameObject shirtCard;

    private Sprite faceOfCard;

    private bool isRotatable;

    private CardType cardType;
    private EnemyData enemyData;
    private WeaponData weaponData;


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

    public WeaponData WeaponData
    {
        get { return weaponData; }
        set { weaponData = value; }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.CHANGE_CARD_SHIRT, ChangeCardShirt);
        Messenger.AddListener(GameEvent.BLOCK_TO_ROTATE, BlockToRotate);
    }

    private void Start()
    {
        shirtCard.SetActive(false);
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
        if (!shirtCard.activeSelf)
            shirtCard.SetActive(true);
        else shirtCard.SetActive(false);
        isRotatable = true;
    }

    public void RotateAndShowFace()
    {
        if (isRotatable)
        {
            StartCoroutine(Rotate());
            Messenger.Broadcast(GameEvent.BLOCK_TO_ROTATE);
        }
            
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

        ChangeCardShirt();

        angle = transform.eulerAngles.y;
        while (angle > 0f)
        {
            angle = Mathf.LerpAngle(angle, -1f, Time.time / 30);
            transform.eulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
            yield return null;
        }

        isRotatable = false;
    }

    public void SetType(CardType sctructCardType)
    {
        try
        {
            switch (sctructCardType)
            {
                case CardType.ENEMY:
                {
                    List<EnemyData> enemies = GameObject.Find("LevelController").GetComponent<LevelController>()
                        .CurrentLevel
                        .Enemies;
                    enemyData = enemies[Random.Range(0, enemies.Count)];
                    UpdateFaceCard(CardType.ENEMY);
                    break;
                }
                case CardType.WEAPON:
                {
                    List<WeaponData> weapons = GameObject.Find("LevelController").GetComponent<LevelController>()
                        .CurrentLevel
                        .Weapons;
                    weaponData = weapons[Random.Range(0, weapons.Count)];
                    UpdateFaceCard(CardType.WEAPON);
                    break;
                }
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void UpdateFaceCard(CardType cardType)
    {
        var other = faceCard.transform.Find("Other");
        switch (cardType)
        {
            case CardType.ENEMY:
            {
                faceCard.GetComponent<Image>().sprite = enemyData.SpriteEnemy;
                other.Find("TextName").GetComponent<TextMeshProUGUI>().text = enemyData.NameEnemy;
                other.Find("TextLeftAngle").GetComponent<TextMeshProUGUI>().text = enemyData.HpEnemy.ToString();
                other.Find("TextRightAngle").GetComponent<TextMeshProUGUI>().text = enemyData.AttackEnemy.ToString();
                break;
            }
            case CardType.WEAPON:
            {
                faceCard.GetComponent<Image>().sprite = weaponData.SpriteWeapon;
                other.Find("TextName").GetComponent<TextMeshProUGUI>().text = weaponData.NameWeapon;
                other.Find("TextRightAngle").GetComponent<TextMeshProUGUI>().text = weaponData.AttackWeapon.ToString();
                break;
            }
        }
    }
}