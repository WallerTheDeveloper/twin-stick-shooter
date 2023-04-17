using System;
using UnityEngine;

namespace Code.Entities
{
    public class Health
    {
        private float _maxHealth;
        private float _currentHealth;

        public event Action OnHealthChanged;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
        
        public void Decrease(float value)
        {
            _currentHealth -= value;

            OnHealthChanged?.Invoke();
            
            Debug.Log($"Decreased {value} current health = {_currentHealth}");
        }
    }
}