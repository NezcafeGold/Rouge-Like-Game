using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "AbilityData")]
    [Serializable]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] public string NameAbility;
        [SerializeField][TextArea] public string DescriptionAbility;
        [SerializeField] public AbilitityEnum AbulityEnum;
        [SerializeField] public int LuckCount;
        [SerializeField] public int CellLuckCount;

        public enum AbilitityEnum
        {
            [Tooltip("Это такой-то спелл")]PUK,
            [Tooltip("Это другой спелл")]COCK,
            KNIFE
        }
    }
}