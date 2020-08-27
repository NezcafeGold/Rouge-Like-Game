using UnityEngine;
using UnityEngine.Events;

public class DiceField : MonoBehaviour
{
    [SerializeField] private Dice dice;

    private int diceCount;
    private bool isEnable;
    public Side side;
    public UnityEvent ShowBattleButton;

    // Start is called before the first frame update
    void Start()
    {
        isEnable = true;
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.BATTLE_START, GenerateDices);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BATTLE_START, GenerateDices);
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

    public void RollTheDiceEnemy()
    {
        if (side == Side.ENEMY && isEnable)
        {
            RollTheDices();
            
        }
    }

    public void RollTheDices()
    {
        if (isEnable)
        {
            foreach (Transform tr in transform)
            {
                tr.GetComponent<Dice>().DiceRoll(side);
            }

            isEnable = false;
            ShowBattleButton.Invoke();
        }
    }
}