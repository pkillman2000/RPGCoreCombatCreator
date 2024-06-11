using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    /*
     * This contains the various actions that a fighter can perform
    */
    public class Fighter : MonoBehaviour, IAction
    {
        private Transform _target;
        
        [SerializeField]
        private float _weaponRange;
        [SerializeField]
        private float _weaponDamage;
        [SerializeField]
        private float _timeBetweenAttacks;
        private float _timeSinceLastAttack;

        private Mover _mover;
        private ActionScheduler _actionScheduler;
        private Animator _animator;




        void Start()
        {
            _mover = GetComponent<Mover>();
            if(_mover == null)
            {
                Debug.LogError("Mover is Null!");
            }

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
            {
                Debug.LogError("Action Scheduler is Null!");
            }

            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator is Null!");
            }
        }


        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            // If no target, leave
            if (_target == null)
            {
                return;
            }
            
            // Move until within weapon range
            if (_target != null && !GetIsInRange())
            {
                _mover.MoveTo(_target.position);                
            }
            else
            {
                _mover.Cancel();
                AttackBehavior();
            }
        }

        // Compare current distance vs weapon range
        private bool GetIsInRange()
        {
            return Vector3.Distance(this.transform.position, _target.position) <= _weaponRange;
        }

        // Attack target
        public void Attack(CombatTarget target)
        {
            _target = target.transform;
        }

        // Cancel attacking target
        public void Cancel()
        {
            _target = null;
        }

        // Animation for attacking
        private void AttackBehavior()
        {

            _actionScheduler.StartAction(this);

            // Throttles the frequency of attacks
            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                _animator.SetTrigger("attack");                
                _timeSinceLastAttack = 0;
            }
        }

        /*
         * Animation event - called by animation
         * This is the point in the animation where the character
         * actually hits the target.  This can be used to start particle
         * effects, sound effects, etc
        */
        public void Hit()
        {
            Health health = _target.GetComponent<Health>();
            if (health == null)
            {
                Debug.LogError("Health is Null!");
            }
            
            health.TakeDamage(_weaponDamage);
        }

    }
}
