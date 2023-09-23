using UnityEngine;
using UnityEngine.Events;

namespace Sc.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected float damage;
        public UnityEvent changeEvent;
        
        public virtual void Attack(LayerMask enemyLayer)
        {
            print("Attack");
        }

        public virtual void Equip()
        {
            gameObject.SetActive(true);
        }

        public virtual void UnEquip()
        {
            gameObject.SetActive(false);
        }
    }
}
