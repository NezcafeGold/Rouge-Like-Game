using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardScr : MonoBehaviour
{
    [SerializeField] private Sprite shirtOfCard;

    [SerializeField] private GameObject faceCard;
    [SerializeField] private GameObject shirtCard;
    [SerializeField] private GameObject description;

    private Sprite faceOfCard;

    private bool isRotatable;
    private bool isChosenCard;

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
        Messenger.AddListener(GameEvent.HANDLE_CHOSEN_CARD, HandleChosenCard);
        Messenger.AddListener(GameEvent.ALLOW_TO_ROTATE, AllowToRotate);
    }

    private void Start()
    {
        shirtCard.SetActive(false);
        description.SetActive(false);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CHANGE_CARD_SHIRT, ChangeCardShirt);
        Messenger.RemoveListener(GameEvent.BLOCK_TO_ROTATE, BlockToRotate);
        Messenger.RemoveListener(GameEvent.HANDLE_CHOSEN_CARD, HandleChosenCard);
        Messenger.AddListener(GameEvent.ALLOW_TO_ROTATE, AllowToRotate);
        
        
        
    }

    private void BlockToRotate()
    {
        isRotatable = false;
    }

    private void AllowToRotate()
    {
        isRotatable = true;
    }

    private void ChangeCardShirt()
    {
        if (!shirtCard.activeSelf)
            shirtCard.SetActive(true);
        else shirtCard.SetActive(false);
        //isRotatable = true;
    }

    public void RotateAndShowFace()
    {
        if (isRotatable)
        {
            StartAnimationRotate();
            Messenger.Broadcast(GameEvent.BLOCK_TO_ROTATE);
            Messenger.Broadcast(GameEvent.ACTIVE_ACCEPT_BUTTON);
            isChosenCard = true;
        }
        else
        {
            if (!description.activeSelf)
                description.SetActive(true);
            else description.SetActive(false);
        }
    }


    private void StartAnimationRotate()
    {
        isRotatable = false;
        GetComponent<Animation>().Play(GetComponent<Animation>().clip.name);
    }

    public void SetType(CardType sctructCardType)
    {
        try
        {
            switch (sctructCardType)
            {
                case CardType.ENEMY:
                {
                    List<EnemyData> enemies = LevelController.Instance.GetCurrentLevel().Enemies;
                    enemyData = enemies[Random.Range(0, enemies.Count)];
                    UpdateFaceCard(CardType.ENEMY);
                    break;
                }
                case CardType.WEAPON:
                {
                    List<WeaponData> weapons = LevelController.Instance.GetCurrentLevel().Weapons;
                    weaponData = weapons[Random.Range(0, weapons.Count)];
                    UpdateFaceCard(CardType.WEAPON);
                    break;
                }
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
        this.cardType = cardType;
        switch (cardType)
        {
            case CardType.ENEMY:
            {
                faceCard.GetComponent<Image>().sprite = enemyData.SpriteEnemy;
                other.Find("TextName").GetComponent<TextMeshProUGUI>().text = enemyData.NameEnemy;
                other.Find("TextLeftAngle").GetComponent<TextMeshProUGUI>().text = enemyData.HpEnemy.ToString();
                other.Find("TextRightAngle").GetComponent<TextMeshProUGUI>().text = enemyData.AttackEnemy.ToString();
                description.GetComponentInChildren<TextMeshProUGUI>().text = enemyData.Description;
                break;
            }
            case CardType.WEAPON:
            {
                faceCard.GetComponent<Image>().sprite = weaponData.SpriteWeapon;
                other.Find("TextName").GetComponent<TextMeshProUGUI>().text = weaponData.NameWeapon;
                other.Find("TextRightAngle").GetComponent<TextMeshProUGUI>().text = weaponData.AttackWeapon.ToString();
                description.GetComponentInChildren<TextMeshProUGUI>().text = weaponData.Description;
                break;
            }
        }
    }

    private void HandleChosenCard()
    {
        if (isChosenCard)
        {
            switch (cardType)
            {
                case CardType.WEAPON:
                    Messenger.Broadcast<WeaponData>(GameEvent.ADD_ITEM_TO_INVENTORY, weaponData);
                    break;
                case CardType.ENEMY:
                    Messenger.Broadcast<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, gameObject);
                    Messenger.Broadcast(GameEvent.BATTLE_START_ANIM);
                    break;
            }
        }
    }
}