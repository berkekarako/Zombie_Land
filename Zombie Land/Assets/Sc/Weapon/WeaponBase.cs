using System;
using Sc.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Sc.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected float damage;
        public UnityEvent changeEvent;
        
        private WeaponSystem _weaponSystem;
        
        public virtual void Attack(LayerMask enemyLayer)
        {
            print("Attack");
        }

        public virtual void Equip()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnUpdate() {}
        
        public virtual void UnEquip()
        {
            gameObject.SetActive(false);
        }
    }
}
