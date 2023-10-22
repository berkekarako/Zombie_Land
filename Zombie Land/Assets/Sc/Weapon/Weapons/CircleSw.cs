using System.Collections;
using Sc.GeneralSystem;
using Sc.Interfaces;
using UnityEngine;

namespace Sc.Weapon.Weapons
{
    public class CircleSw : Swords
    {
        [SerializeField] private float rad = 2f;
        [SerializeField] private GameObject attackLight;
        
        private Collider2D[] _hits = new Collider2D[5];

        public override void Attack()
        {
            base.Attack();

            Physics2D.OverlapCircleNonAlloc(transform.position, rad, _hits, enemyLayer);

            HealthDamageSys.TakeDamage(_hits, damage);
            
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
