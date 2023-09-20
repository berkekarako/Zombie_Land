using System;
using System.Collections;
using Pathfinding;
using Sc.Interfaces;
using Sc.Player;
using UnityEngine;

namespace Sc.Enemy
{
    public class Zombie : MonoBehaviour, IDamageable
    {
        [SerializeField] private float speed = 2.5f;
        
        private void Start()
        {
            GetComponent<AIPath>().maxSpeed = speed;
            CurrentHealth = maxHealth;
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (_canAttack && other.gameObject.CompareTag("Player"))
                StartCoroutine(Attack(other.gameObject));
        }

        #region Health

        [Header("Health")]
        [SerializeField] private float maxHealth = 100f;
        
        public float CurrentHealth { get; set; }
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            
            if(0 >= CurrentHealth)
                Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
        
        #endregion
        
        #region Attack

        [Header("Attack")]
        [SerializeField] private float damage = 20;
        [SerializeField] private float attackDuration = 1f;
        
        private bool _canAttack = true;
        
        private IEnumerator Attack(GameObject player)
        {
            _canAttack = false;
            player.GetComponent<PlayerHealth>().TakeDamage(damage);

            yield return new WaitForSeconds(attackDuration);
            _canAttack = true;
        }

        #endregion
    }
}
