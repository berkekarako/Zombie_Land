using System.Collections;
using Sc.Player;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

namespace Sc.Enemy
{
    public class Zombie : Enemy
    {
        [Header("C/Attack")]
        [SerializeField] private float damage = 20;
        [SerializeField] private float attackDuration = 1f;
        private bool _canAttack = true;
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (_canAttack && other.gameObject.CompareTag("Player"))
                StartCoroutine(Attack());
        }
        
        private IEnumerator Attack()
        {
            _canAttack = false;
            
            Player.Player.Instance.PlayerHealth.TakeDamage(damage);
            AiPath.canMove = false;
            Rb.constraints = RigidbodyConstraints2D.FreezeAll;

            yield return new WaitForSeconds(attackDuration);
            _canAttack = true;
            AiPath.canMove = true;
            Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public override void TakeDamage(float damageCount)
        {
            base.TakeDamage(damageCount);
            
            healthSlider.value = CurrentHealth / maxHealth;
        }
    }
}
