using Pathfinding;
using Sc.Interfaces;
using UnityEngine;

namespace Sc.Enemy
{
    [RequireComponent(typeof(Seeker), typeof(AIPath) ,typeof(AIDestinationSetter))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        protected Rigidbody2D Rb;
        private AIDestinationSetter AIDestinationSetter;
        
        [Header("Health")]
        [SerializeField] protected float maxHealth = 100f;
        public float CurrentHealth { get; set; }
        
        [Header("Movement")]
        [SerializeField] private float speed = 2.5f;
        protected AIPath AiPath;
        
        private void Start()
        {
            AiPath = GetComponent<AIPath>();
            Rb = GetComponent<Rigidbody2D>();

            AIDestinationSetter = GetComponent<AIDestinationSetter>();
            AIDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
            
            AiPath.maxSpeed = speed;
            CurrentHealth = maxHealth;
        }
        
        public virtual void TakeDamage(float damageCount)
        {
            CurrentHealth -= damageCount;
            
            if(0 >= CurrentHealth)
                Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
