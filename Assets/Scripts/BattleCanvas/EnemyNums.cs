using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EnemyNums : MonoBehaviour, INums
{
    private GameObject enemyCard;
    public int Attack;
    public int HP;
    public int DiceAmount;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.UPDATE_ENEMY_STATS, UpdateVisualStats);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.UPDATE_ENEMY_STATS, UpdateVisualStats);
    }

    private void UpdateVisualStats()
    {
        Attack = EnemySetup.Instance.Attack;
        HP = EnemySetup.Instance.CurrentLive;
        DiceAmount = EnemySetup.Instance.DiceCount;
        
        transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = Attack.ToString();
        transform.Find("HP").Find("HPNum").GetComponent<TextMeshProUGUI>().text = HP.ToString();
        transform.Find("Dice").Find("DiceNum").GetComponent<TextMeshProUGUI>().text = DiceAmount.ToString();
    }

    public void AddAttack(int value)
    {
        Attack += value;
        UpdateVisualStats();
    }

    public void SubtractAttack(int value)
    {
        Attack -= value;
        UpdateVisualStats();
    }

    public void AddHP(int value)
    {
        HP += value;
        UpdateVisualStats();
    }

    public void SubtractHP(int value)
    {
        HP -= value;
        UpdateVisualStats();
    }

    public void AddDice(int value)
    {
        DiceAmount += value;
        UpdateVisualStats();
    }

    public void SubtractDice(int value)
    {
        DiceAmount -= value;
        UpdateVisualStats();
    }
}