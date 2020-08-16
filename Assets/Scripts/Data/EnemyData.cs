using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string nameEnemy;
        [SerializeField] private Sprite spriteEnemy;
        [SerializeField] private int hpEnemy;
        [SerializeField] private int attackEnemy;
        [SerializeField] private int diceAmount;
        [SerializeField] private string description = "";
        [SerializeField] private List<AbilityData> abilityData;

        public string NameEnemy
        {
            get { return nameEnemy; }
            set { nameEnemy = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Sprite SpriteEnemy
        {
            get { return spriteEnemy; }
            set { spriteEnemy = value; }
        }

        public int HpEnemy
        {
            get { return hpEnemy; }
            set { hpEnemy = value; }
        }

        public int AttackEnemy
        {
            get { return attackEnemy; }
            set { attackEnemy = value; }
        }

        public List<AbilityData> AbilityData
        {
            get { return abilityData; }
            set { abilityData = value; }
        }

        public int DiceAmount
        {
            get { return diceAmount; }
            set { diceAmount = value; }
        }
    }
}