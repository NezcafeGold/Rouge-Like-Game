using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "AbilityData")]
    [Serializable]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] public string NameAbility;
        [SerializeField] [TextArea] public string DescriptionAbility;
        [SerializeField] public AbilitityEnum AbilityEnum;
        [SerializeField] 
        [Header("Число, которое используется в описании! Важно, чтобы в описании число совпадало с этим")]
        public int ValueForAbility;
        [SerializeField] public int LuckCount;
        [SerializeField] public int CellLuckCount;
        [SerializeField] public ColorEnum Color;

        public enum AbilitityEnum
        {
            [Tooltip("Урон за успех")] DAMAGE_FOR_LUCK,
            [Tooltip("Защита за успех")] DEFENCE_FOR_LUCK,
            [Tooltip("Уворот за успех")] DODGE_FOR_LUCK,
            [Tooltip("Сжечь вражеский куб за успех")] BURN_DICE_FOR_LUCK
        }

    }
}