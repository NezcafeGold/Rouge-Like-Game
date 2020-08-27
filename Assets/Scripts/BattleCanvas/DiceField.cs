using System.Collections;
using System.Collections.Generic;
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
        Messenger.AddListener(GameEvent.ENEMY_SETUP_TURN, RollTheDiceEnemy);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BATTLE_START, GenerateDices);
        Messenger.RemoveListener(GameEvent.ENEMY_SETUP_TURN, RollTheDiceEnemy);
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
            StartCoroutine(RollTheDicesCourotine());

            BattleController.Instance.NextTurn();
        }
    }

    private IEnumerator RollTheDicesCourotine()
    {
        RollTheDices();
        yield return new WaitForSeconds(2f);
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