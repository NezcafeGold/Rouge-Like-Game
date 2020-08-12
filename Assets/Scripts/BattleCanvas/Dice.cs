using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // Array of dice sides sprites to load from Resources folder
    [SerializeField] private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image rend;

    private bool isEnableToRoll;

    // Use this for initialization
    private void Start()
    {
        // Assign Renderer component
        rend = GetComponent<Image>();
        isEnableToRoll = true;
    }

    public void DiceRoll()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        Debug.Log(finalSide);

        ColorEnum colorEnum = ColorEnum.NONE;
        switch (finalSide)
        {
            case 1:
                colorEnum = ColorEnum.RED;
                break;
            case 2:
                colorEnum = ColorEnum.GREEN;
                break;
            case 3:
                colorEnum = ColorEnum.BLUE;
                break;
            case 4:
                colorEnum = ColorEnum.YELLOW;
                break;
            default:
                colorEnum = ColorEnum.NONE;
                break;
        }

        Messenger.Broadcast<ColorEnum>(GameEvent.ADD_DICE_SIDE, colorEnum);
        Messenger.Broadcast(GameEvent.SHOW_BATTLE_CONTROLLER);
    }
}