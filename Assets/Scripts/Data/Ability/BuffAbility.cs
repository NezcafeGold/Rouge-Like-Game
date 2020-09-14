using UnityEngine;

namespace DefaultNamespace.Ability
{
    [CreateAssetMenu(menuName = "Ability/BuffAbility")]
    public class BuffAbility : AbilityData
    {
        public override void Use()
        {
            if (BattleController.Instance.CURRENT_TURN == 2) //BEFORE FIGHT
            {
                Debug.Log("BuffAbility");
            }
        }
    }
}