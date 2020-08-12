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

    public int CurrentPointsFromDices
    {
        get { return currentPointsFromDices; }
        set { currentPointsFromDices = value; }
    }
    
    //TODO: реализовать GameEvent.BATTLE_ENEMY_TURN, вложенный класс с методом который вызывает метод по енам 

    void Awake()
    {
        Messenger.AddListener<ColorEnum>(GameEvent.ADD_DICE_SIDE, AddDiceSide);
        Messenger.AddListener(GameEvent.BATTLE_ENEMY_TURN, ApplyStaminaPoints);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<ColorEnum>(GameEvent.ADD_DICE_SIDE, AddDiceSide);
    }

    private void Start()
    {
        pointsOnChargePanel = 0;
        valueCharge = 0;
        freePointsFromStamina = 0;
        currentPointsFromDices = 0;
        isEnableToAddPoints = false;
        resetButton = transform.Find("Reset").gameObject;
        resetButton.SetActive(false);
    }

    private void ApplyStaminaPoints()
    {
        currentPointsFromDices = currentPointsFromDices + freePointsFromStamina;
        freePointsFromStamina = 0;
        resetButton.SetActive(false);
    }

    private void AddPointsOnChargePanel()
    {
        pointsOnChargePanel++;
        if (pointsOnChargePanel >= abilityData.LuckCount)
        {
            valueCharge = (int) pointsOnChargePanel / abilityData.LuckCount;
        }
    }

    public void AddPointsFromStamina()
    {
        if (isEnableToAddPoints && PlayerSetup.GetPlayerSetup().CurrentStaminaPoints > 0 &&
            freePointsFromStamina < abilityData.CellLuckCount - currentPointsFromDices)
        {
            PlayerSetup.GetPlayerSetup().SubtractStamina(1);
            freePointsFromStamina++;
            UpdatePointsVisual(false);
            resetButton.SetActive(true);
        }
    }

    public void ResetPointsFromStamina()
    {
        PlayerSetup.GetPlayerSetup().AddStamina(freePointsFromStamina);
        freePointsFromStamina = 0;
        resetButton.SetActive(false);
        UpdatePointsVisual(true);
    }

    private void AddDiceSide(ColorEnum colorEnum)
    {
        isEnableToAddPoints = true;
        if (abilityData.Color == colorEnum)
        {
            currentPointsFromDices++;
            AddPointsOnChargePanel();
            UpdatePointsVisual(false);
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

    private class AbilitySpell
    {
               
    }
}