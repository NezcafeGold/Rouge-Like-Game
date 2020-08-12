using System.Collections;
using System.Collections.Generic;
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
        diceCount = PlayerSetup.GetPlayerSetup().DiceCount;
        GenerateDices();
        isEnable = true;
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.BATTLE_ENEMY_TURN, RollTheDiceFromMessenger );
    }

    private void GenerateDices()
    {
        for (int i = 0; i < diceCount; i++)
        {
            Instantiate(dice, transform);
        }
    }

    private void RollTheDiceFromMessenger()
    {
        if (side == Side.ENEMY)
            RollTheDices();
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