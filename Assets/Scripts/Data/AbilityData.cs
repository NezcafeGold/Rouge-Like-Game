using System;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData")]
[Serializable]
public class AbilityData : ScriptableObject
{
    [SerializeField] public int Id;
    [SerializeField] public string NameAbility;
    [SerializeField] [TextArea] public string DescriptionAbility;
    [SerializeField] public AbilitityWhatEnum abilityWhatEnum;
    [SerializeField] public AbilityWhenEnum AbilityWhenEnum;
    [SerializeField] public AbilityDoEnum AbilityDoEnum;

    [SerializeField] [Header("Число, которое используется в описании! Важно, чтобы в описании число совпадало с этим")]
    public int ValueForAbility;

    [SerializeField] public int LuckCount;
    [SerializeField] public int CellLuckCount;
    [SerializeField] public ColorEnum Color;
}