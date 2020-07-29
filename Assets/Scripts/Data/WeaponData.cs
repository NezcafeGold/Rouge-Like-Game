using UnityEngine;


namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField]  private string nameWeapon;
        [SerializeField]  private Sprite spriteWeapon;
        [SerializeField]  private int attackWeapon;

        public string NameWeapon
        {
            get { return nameWeapon; }
            set { nameWeapon = value; }
        }

        public Sprite SpriteWeapon
        {
            get { return spriteWeapon; }
            set { spriteWeapon = value; }
        }

        public int AttackWeapon
        {
            get { return attackWeapon; }
            set { attackWeapon = value; }
        }
    }
}