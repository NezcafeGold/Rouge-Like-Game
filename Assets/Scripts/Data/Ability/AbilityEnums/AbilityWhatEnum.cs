using UnityEngine;

public enum AbilitityWhatEnum
{
    [Tooltip("Урон за успех")] ATTACK,
    [Tooltip("Защита за успех")] HEALTH,
    [Tooltip("Уворот за успех")] DODGE,

    [Tooltip("Сжечь вражеский куб за успех")]
    DICE,
    STAMINA
}