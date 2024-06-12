using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _healthPoints;

        private bool _isAlive = true;
        private Animator _animator;
        private CapsuleCollider _collider;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if(_animator == null)
            {
                Debug.LogError("Animator is Null!");
            }

            _collider = GetComponent<CapsuleCollider>();
            if(_collider == null)
            {
                Debug.LogError("Collider is Null!");
            }

            /*
             * Make sure the collider is active.
             * If this character has already died, the collider
             * is probably turned off.  This will make sure that
             * the collider is active if this character is respawned.
            */
            if(_collider.enabled == false)
            {
                _collider.enabled = true;
            }
        }

        public void TakeDamage(float damage)
        {
            if (_isAlive)
            {
                // This stops the value from dropping below 0
                _healthPoints = Mathf.Max(_healthPoints - damage, 0);

                if (_healthPoints == 0)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            _isAlive = false;
            /*
             * Disable the collider so that raycast doesn't see this character anymore.
             * This may break things later if we wish to respawn this character.
             * Remember to turn the collider back on.
            */
            _collider.enabled = false;
            _animator.SetTrigger("die");
        }

        public bool IsAlive()
        { 
            return _isAlive; 
        }
    }
}
