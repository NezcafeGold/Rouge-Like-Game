using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AbilityPlayer : MonoBehaviour
{
    [SerializeField] private Ability abilityPrefab;

    private List<AbilityData> abilitiesData;

    void Start()
    {
        abilitiesData = GameObject.Find("PlayerSetup").GetComponent<PlayerSetup>().Abilities;
        GenerateSpellBook();
    }

    private void GenerateSpellBook()
    {
        foreach (var ab in abilitiesData)
        {
            var obj = Instantiate(abilityPrefab);
            obj.GetComponent<Ability>().SetData(ab);
        }
    }
}