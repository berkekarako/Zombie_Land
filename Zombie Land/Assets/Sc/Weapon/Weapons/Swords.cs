using UnityEngine;

namespace Sc.Weapon.Weapons
{
    public class Swords : WeaponBase
    {
        [SerializeField] protected float damage;
        [SerializeField] protected LayerMask enemyLayer;
    }
}
