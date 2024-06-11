using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _health;

        public void TakeDamage(float damage)
        {
            if (_health > 0)
            {
                // This stops the value from dropping below 0
                _health = Mathf.Max(_health - damage, 0);
                Debug.Log("Health: " + _health);
            }
        }
    }
}
