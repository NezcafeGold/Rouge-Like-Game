using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    [SerializeField] private Ability abilityPrefab;
    private List<AbilityData> abilitiesData;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        abilitiesData = GameObject.Find("PlayerSetup").GetComponent<PlayerSetup>().Abilities;
        GenerateSpellBook();
    }

    private void GenerateSpellBook()
    {
        foreach (var ab in abilitiesData)
        {
            var obj = Instantiate(abilityPrefab, transform);
            obj.GetComponent<Ability>().SetData(ab);
        }
    }
}