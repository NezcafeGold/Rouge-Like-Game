using TMPro;
using UnityEngine;

namespace EquipInventory
{
    public class PlayerNums : MonoBehaviour, INums
    {
        private int attack;
        private int beforeAttack;
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
            PlayerSetup playerSetup = PlayerSetup.Instance;
            if (attack != playerSetup.Attack)
            {
                var v = playerSetup.Attack - attack;
                if (attack != 0)
                    Messenger.Broadcast<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, AbilitityWhatEnum.ATTACK,
                        v);
                attack = playerSetup.Attack;
            }

            if (currentLive != playerSetup.CurrentLive)
            {
                var v = playerSetup.CurrentLive - currentLive;
                if (currentLive != 0)
                    Messenger.Broadcast<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, AbilitityWhatEnum.HEALTH,
                        v);
                currentLive = playerSetup.CurrentLive;
            }

            if (diceCount != playerSetup.DiceCount)
            {
                var v = playerSetup.DiceCount - diceCount;
                if (diceCount != 0)
                    Messenger.Broadcast<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, AbilitityWhatEnum.DICE, v);
                diceCount = playerSetup.DiceCount;
            }

            totalLive = playerSetup.TotalLives;

            if (currentStamina != playerSetup.CurrentStaminaPoints)
            {
                var v = playerSetup.CurrentStaminaPoints - currentStamina;
                if (currentStamina != 0)
                    Messenger.Broadcast<AbilitityWhatEnum, int>(GameEvent.ANIM_PLAYER_VALUE, AbilitityWhatEnum.STAMINA,
                        v);
                currentStamina = playerSetup.CurrentStaminaPoints;
            }

            totalStamina = playerSetup.TotalStaminaPoints;

            beforeAttack = attack;

            transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = attack.ToString();
            transform.Find("Live").Find("LiveNum").GetComponent<TextMeshProUGUI>().text = currentLive + "/" + totalLive;
            transform.Find("Cube").Find("CubeNum").GetComponent<TextMeshProUGUI>().text = diceCount.ToString();
            transform.Find("Stamina").Find("StaminaNum").GetComponent<TextMeshProUGUI>().text =
                currentStamina + "/" + totalStamina;
        }


        public void AddAttack(int value)
        {
            attack += value;
            transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = attack.ToString();
        }

        public void SubtractAttack(int value)
        {
            attack -= value;
            transform.Find("Attack").Find("AttackNum").GetComponent<TextMeshProUGUI>().text = attack.ToString();
        }

        public void AddHP(int value)
        {
            currentLive += value;
            if (currentLive >= totalLive)
            {
                currentLive = totalLive;
            }

            PlayerSetup.Instance.CurrentLive = currentLive;
            transform.Find("Live").Find("LiveNum").GetComponent<TextMeshProUGUI>().text = currentLive + "/" + totalLive;
        }

        public void SubtractHP(int value)
        {
            currentLive -= value;
            if (currentLive <= 0)
            {
                currentLive = 0;
            }

            PlayerSetup.Instance.CurrentLive = currentLive;
            transform.Find("Live").Find("LiveNum").GetComponent<TextMeshProUGUI>().text = currentLive + "/" + totalLive;
        }

        public void AddDice(int value)
        {
            diceCount += value;
            transform.Find("Cube").Find("CubeNum").GetComponent<TextMeshProUGUI>().text = diceCount.ToString();
        }

        public void SubtractDice(int value)
        {
            diceCount -= value;
            if (diceCount <= 0) diceCount = 0;
            transform.Find("Cube").Find("CubeNum").GetComponent<TextMeshProUGUI>().text = diceCount.ToString();
        }
    }
}