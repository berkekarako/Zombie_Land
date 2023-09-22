using System.Collections;
using Sc.Interfaces;
using UnityEngine;

namespace Sc.Weapon.Weapons
{
    public class CircleSw : WeaponBase
    {
        [SerializeField] private float rad = 2f;
        [SerializeField] private GameObject attackLight;

        public override void Attack(LayerMask enemyLayer)
        {
            base.Attack(enemyLayer);

            Collider2D hit = Physics2D.OverlapCircle(transform.position, rad, enemyLayer);

            if(hit)
                hit.GetComponent<IDamageable>()?.TakeDamage(damage);
            
            StartCoroutine(AttackLight());
        }
        
        IEnumerator AttackLight()
        {
            attackLight.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            attackLight.SetActive(false);
        }
    }
}
