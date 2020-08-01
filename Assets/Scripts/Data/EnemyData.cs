using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
      [SerializeField]  private string nameEnemy;
      [SerializeField]  private Sprite spriteEnemy;
      [SerializeField]  private int hpEnemy;
      [SerializeField]  private int attackEnemy;
      [SerializeField]  private string description = "";

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
    }
}