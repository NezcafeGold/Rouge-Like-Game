using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private ChargePoint chargePoint;

    private AbilityData abilityData;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        for (int i = 0; i < abd.CellLuckCount; i++)
        {
            Instantiate(chargePoint, chargePanel);
        }
    }

    private Color GetColor(AbilityData.ColorEnum colorEnum)
    {
        Color color = Color.white;
        switch (colorEnum)
        {
            case AbilityData.ColorEnum.RED:
                color = Color.red;
                break;
            case AbilityData.ColorEnum.BLUE:
                color = Color.blue;
                break;
            case AbilityData.ColorEnum.YELLOW:
                color = Color.yellow;
                break;
            case AbilityData.ColorEnum.GREEN:
                color = Color.green;
                break;
            default:
                break;
        }

        return color;
    }
}