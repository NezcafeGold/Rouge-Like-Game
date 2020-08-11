using TMPro;
using UnityEngine;

namespace EquipInventory
{
    public class PlayerNums : MonoBehaviour
    {
        private int attack;
        private int currentLive;
        private int totalLive;
        private int diceCount;
        private int currentStamina;
        private int totalStamina;

        private void Awake()
        {
            Messenger.AddListener(GameEvent.UPDATE_STATS, UpdatePlayerNums);
        }

        private void Start()
        {
            UpdatePlayerNums();
        }

        private void UpdatePlayerNums()
        {
            attack = PlayerSetup.GetPlayerSetup().Attack;
            currentLive = PlayerSetup.GetPlayerSetup().CurrentLive;
            totalLive = PlayerSetup.GetPlayerSetup().TotalLives;
            diceCount = PlayerSetup.GetPlayerSetup().DiceCount;
            currentStamina = PlayerSetup.GetPlayerSetup().CurrentStaminaPoints;
            totalStamina = PlayerSetup.GetPlayerSetup().TotalStaminaPoints;
            
            transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = attack.ToString();
            transform.Find("Live").Find("LiveNum").GetComponent<TextMeshProUGUI>().text = currentLive + "/" + totalLive;
            transform.Find("Cube").Find("CubeNum").GetComponent<TextMeshProUGUI>().text = diceCount.ToString();
            transform.Find("Stamina").Find("StaminaNum").GetComponent<TextMeshProUGUI>().text =
                currentStamina + "/" + totalStamina;
        }
    }
}