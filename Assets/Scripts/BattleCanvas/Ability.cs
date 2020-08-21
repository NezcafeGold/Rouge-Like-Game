using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private ChargePoint chargePoint;
    public Color ColorAb;

    private AbilityData abilityData;

    //сколько очков добавилось
    private int pointsOnChargePanel;

    //сколько зарядов имеется (все очки / очки удачи)
    private int valueCharge;
    private int pointsForLuck;

    private bool isEnableToAddPoints;

    //сколько очков есть, но не примененных
    private int freePointsFromStamina;

    //сколько очков с кубиков
    private int currentPointsFromDices;

    private GameObject resetButton;

    private bool isEnableToClick;
    private Side side;
    private AbilitySpell abilitySpell;

    public int CurrentPointsFromDices
    {
        get { return currentPointsFromDices; }
        set { currentPointsFromDices = value; }
    }

    //TODO: реализовать GameEvent.BATTLE_ENEMY_TURN, вложенный класс с методом который вызывает метод по енам 

    void Awake()
    {
        Messenger.AddListener<ColorEnum>(GameEvent.ADD_DICE_SIDE_FOR_PLAYER, AddDiceSideForPlayer);
        Messenger.AddListener<ColorEnum>(GameEvent.ADD_DICE_SIDE_FOR_ENEMY, AddDiceSideForEnemy);
        Messenger.AddListener(GameEvent.BATTLE_ENEMY_SETUP_TURN, ApplyStaminaPoints);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<ColorEnum>(GameEvent.ADD_DICE_SIDE_FOR_PLAYER, AddDiceSideForPlayer);
        Messenger.RemoveListener<ColorEnum>(GameEvent.ADD_DICE_SIDE_FOR_ENEMY, AddDiceSideForEnemy);
        Messenger.RemoveListener(GameEvent.BATTLE_ENEMY_SETUP_TURN, ApplyStaminaPoints);
    }

    private void Start()
    {
        isEnableToClick = true;
        pointsOnChargePanel = 0;
        valueCharge = 0;
        freePointsFromStamina = 0;
        currentPointsFromDices = 0;
        pointsForLuck = 0;
        isEnableToAddPoints = false;
        resetButton = transform.Find("Reset").gameObject;
        resetButton.SetActive(false);
    }

    private void ApplyStaminaPoints()
    {
        currentPointsFromDices = currentPointsFromDices + freePointsFromStamina;
        freePointsFromStamina = 0;
        resetButton.SetActive(false);
        isEnableToClick = false;
    }

    public void SetSide(Side side)
    {
        this.side = side;
    }

    public void AddPointsFromStamina()
    {
        if (isEnableToAddPoints && PlayerSetup.Instance.CurrentStaminaPoints > 0 &&
            freePointsFromStamina < abilityData.CellLuckCount - currentPointsFromDices && isEnableToClick)
        {
            PlayerSetup.Instance.SubtractStamina(1);
            freePointsFromStamina++;
            UpdatePointsVisual(false);
            resetButton.SetActive(true);
            HandleAbility();
        }
    }

    public void ResetPointsFromStamina()
    {
        PlayerSetup.Instance.AddStamina(freePointsFromStamina);
        freePointsFromStamina = 0;
        resetButton.SetActive(false);
        UpdatePointsVisual(true);

        HandleAbility();
    }

    private void AddDiceSideForPlayer(ColorEnum colorEnum)
    {
        if (side == Side.PLAYER)
        {
            isEnableToAddPoints = true;
            if (abilityData.Color == colorEnum)
            {
                currentPointsFromDices++;
                pointsOnChargePanel++;
                UpdatePointsVisual(false);
            }
        }
    }

    private void AddDiceSideForEnemy(ColorEnum colorEnum)
    {
        if (side == Side.ENEMY)
        {
            isEnableToAddPoints = true;
            if (abilityData.Color == colorEnum)
            {
                currentPointsFromDices++;
                pointsOnChargePanel++;
                UpdatePointsVisual(false);
            }
        }
    }

    private void UpdatePointsVisual(bool needReset)
    {
        var cpObj = gameObject.GetComponent<Ability>().transform.Find("ChargePanel");
        if (needReset)
        {
            foreach (Transform t in cpObj.transform)
            {
                t.Find("Point").gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < pointsOnChargePanel + freePointsFromStamina; i++)
        {
            cpObj.transform.GetChild(i).Find("Point").gameObject.SetActive(true);
        }
    }

    public void SetData(AbilityData abd)
    {
        abilityData = abd;
        Ability ab = gameObject.GetComponent<Ability>();
        ab.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = abd.NameAbility;
        ab.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = abd.DescriptionAbility;
        ab.transform.Find("LuckCount").GetComponent<TextMeshProUGUI>().text = abd.LuckCount.ToString();
        ab.transform.Find("Border").GetComponent<Image>().color = GetColor(abilityData.Color);
        var chargePanel = ab.transform.Find("ChargePanel");
        pointsForLuck = abd.LuckCount;
        for (int i = 0; i < abd.CellLuckCount; i++)
        {
            Instantiate(chargePoint, chargePanel);
        }
    }

    private Color GetColor(ColorEnum colorEnum)
    {
        Color color = Color.white;
        switch (colorEnum)
        {
            case ColorEnum.RED:
                color = Color.red;
                break;
            case ColorEnum.BLUE:
                color = Color.blue;
                break;
            case ColorEnum.YELLOW:
                color = Color.yellow;
                break;
            case ColorEnum.GREEN:
                color = Color.green;
                break;
        }

        ColorAb = color;
        return color;
    }

    public void HandleAbility()
    {
        valueCharge = ((pointsOnChargePanel + freePointsFromStamina) / abilityData.LuckCount);
        if (abilitySpell == null)
        {
            abilitySpell = new AbilitySpell(this);
        }

        abilitySpell.DoAbilitySpell();
    }

    private class AbilitySpell
    {
        private Ability ability;

        public AbilitySpell(Ability ability)
        {
            this.ability = ability;
        }

        public void DoAbilitySpell()
        {
            switch (ability.abilityData.AbilityEnum)
            {
                case AbilityData.AbilitityEnum.ATTACK_GAIN:
                    AttackGain();
                    break;
                case AbilityData.AbilitityEnum.HEALTH_GAIN:
                    HealthGain();
                    break;
                case AbilityData.AbilitityEnum.DODGE_GAIN:
                    DodgeGain();
                    break;
                case AbilityData.AbilitityEnum.DICE_DECREASE:
                    DiceDecrease();
                    break;
            }
        }

        //Усиление атаки
        private void AttackGain()
        {
            if (ability.side == Side.PLAYER)
            {
                PlayerSetup.Instance.AddExtAttack(ability.valueCharge * ability.abilityData.ValueForAbility);
            }
        }

        //Усиление защиты
        private void HealthGain()
        {
            if (ability.side == Side.PLAYER)
            {
                PlayerSetup.Instance.AddExtHealth(ability.valueCharge * ability.abilityData.ValueForAbility);
            }
        }

        //Уклонение
        private void DodgeGain()
        {
            if (ability.side == Side.PLAYER)
            {
                PlayerSetup.Instance.AddExtDodge(ability.valueCharge * ability.abilityData.ValueForAbility);
            }
        }

        //Сжигаение кубика
        private void DiceDecrease()
        {
            if (ability.side == Side.PLAYER)
            {
                PlayerSetup.Instance.AddExtDiceDecrease(ability.valueCharge * ability.abilityData.ValueForAbility);
            }
        }
    }
}