using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Sc.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            
            healthText.text = _currentHealth.ToString(CultureInfo.InvariantCulture);
            
            if(0 >= _currentHealth)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
