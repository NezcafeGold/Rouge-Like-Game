using UnityEngine;

public class DiceField : MonoBehaviour
{
    [SerializeField] private Dice dice;

    private int diceCount;
    private bool isEnable;
    public Side side;

    // Start is called before the first frame update
    void Start()
    {
        isEnable = true;
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.BATTLE_ENEMY_SETUP_TURN, RollTheDiceEnemy);
        Messenger.AddListener(GameEvent.BATTLE_START, GenerateDices);
    }

    private void GenerateDices()
    {
        if (side == Side.PLAYER)
            diceCount = PlayerSetup.Instance.DiceCount;
        else if (side == Side.ENEMY)
            diceCount = transform.parent.Find("EnemyNums").GetComponent<EnemyNums>().DiceAmount;
        
        for (int i = 0; i < diceCount; i++)
        {
            Instantiate(dice, transform);
        }
    }

    private void RollTheDiceEnemy()
    {
        if (side == Side.ENEMY)
            RollTheDices();
        BattleController.NextTurn();
    }

    public void RollTheDices()
    {
        if (isEnable)
            foreach (Transform tr in transform)
            {
                tr.GetComponent<Dice>().DiceRoll(side);
            }

        isEnable = false;
    }
}