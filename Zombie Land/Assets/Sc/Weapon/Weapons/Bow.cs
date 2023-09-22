using UnityEngine;

namespace Sc.Weapon.Weapons
{
    public class Bow : WeaponBase
    {
        public override void Attack(LayerMask enemyLayer)
        {
            base.Attack(enemyLayer);
            print("Bow Attack");
        }
    }
}
