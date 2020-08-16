using DefaultNamespace;
using TMPro;
using UnityEngine;

public class EnemyNums : MonoBehaviour
{
    private GameObject enemyCard;
    public int Attack;
    public int HP;
    public int DiceAmount;

    private void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemy);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, AddEnemy);
    }

    public void AddEnemy(GameObject enemyCard)
    {
        this.enemyCard = enemyCard;

        EnemyData enemyData = enemyCard.GetComponent<CardScr>().EnemyData;
        Attack = enemyData.AttackEnemy;
        HP = enemyData.HpEnemy;
        DiceAmount = enemyData.DiceAmount;
        UpdateVisualStats();
    }

    private void UpdateVisualStats()
    {
        transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = Attack.ToString();
        transform.Find("HP").Find("HPNum").GetComponent<TextMeshProUGUI>().text = HP.ToString();
        transform.Find("Dice").Find("DiceNum").GetComponent<TextMeshProUGUI>().text = DiceAmount.ToString();
    }
}