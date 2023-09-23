using System;
using Sc.Interfaces;
using UnityEngine;

namespace Sc.Weapon.Weapons
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float damage;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHit(other.gameObject);
        }

        protected virtual void OnHit(GameObject obj)
        {
            if (obj.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }
}
