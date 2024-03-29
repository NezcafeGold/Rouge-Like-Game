﻿using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private Ability abilityPrefab;

    private List<AbilityData> abilitiesData;
    public Side side;

    void Awake()
    {
        Messenger.AddListener<GameObject>(GameEvent.ADD_ENEMY_TO_BATTLE, GenerateSpellBookForEnemy);
    }


    void Start()
    {
        if (side == Side.PLAYER)
        {
            abilitiesData = PlayerSetup.Instance.Abilities;
            GenerateSpellBook();
        }
    }


    private void GenerateSpellBook()
    {
        foreach (var ab in abilitiesData)
        {
            var obj = Instantiate(abilityPrefab, transform);
            obj.GetComponent<Ability>().SetData(ab);
            obj.GetComponent<Ability>().SetSide(side);
        }
    }

    private void GenerateSpellBookForEnemy(GameObject go)
    {
        if (side == Side.ENEMY)
        {
            abilitiesData = go.GetComponent<CardScr>().EnemyData.AbilityData;
            GenerateSpellBook();
        }
    }
}