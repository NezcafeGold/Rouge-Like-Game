using DefaultNamespace;
using TMPro;
using UnityEngine;

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
        var chargePanel = ab.transform.Find("ChargePanel");
        for (int i = 0; i < abd.CellLuckCount; i++)
        {
            Instantiate(chargePoint, chargePanel);
        }
    }
}
