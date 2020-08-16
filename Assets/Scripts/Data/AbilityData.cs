using System;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityData")]
[Serializable]
public class AbilityData : ScriptableObject
{
    [SerializeField] public string NameAbility;
    [SerializeField] [TextArea] public string DescriptionAbility;
    [SerializeField] public AbilitityEnum AbilityEnum;

    [SerializeField] [Header("Число, которое используется в описании! Важно, чтобы в описании число совпадало с этим")]
    public int ValueForAbility;

    [SerializeField] public int LuckCount;
    [SerializeField] public int CellLuckCount;
    [SerializeField] public ColorEnum Color;

    public enum AbilitityEnum
    {
        [Tooltip("Урон за успех")] ATTACK_GAIN,
        [Tooltip("Защита за успех")] HEALTH_GAIN,
        [Tooltip("Уворот за успех")] DODGE_GAIN,

        [Tooltip("Сжечь вражеский куб за успех")]
        DICE_DECREASE
    }
}