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
        private Health _target;
        
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

            // If target is dead, leave
            if (!_target.IsAlive())
            {
                return;
            }

            // Move until within weapon range
            if (_target != null && !GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position);                
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
            return Vector3.Distance(this.transform.position, _target.transform.position) <= _weaponRange;
        }

        // Check to see if valid target
        public bool CanAttack(CombatTarget target)
        {
            /*
             * Check to see if target is null.  If so,
             * return false.
             * 
             * Note: This is testing against CombatTarget script.  Later
             * we are testing vs the Health script.
            */
            if(target == null)
            {
                return false;
            }

            Health targetToTest = target.GetComponent<Health>();

            /*
             * If target is not null and target is alive,
             * return true.  Otherwise, return false.
             * 
             * Note: This is now testing against the target having a Health
             * script attached.
            */
            return targetToTest != null && targetToTest.IsAlive();
        }

        // Attack target
        public void Attack(CombatTarget target)
        {
            _target = target.GetComponent<Health>();
        }

        // Cancel attacking target
        public void Cancel()
        {
            _target = null;
            StopAttack();
        }

        // Animation for attacking
        private void AttackBehavior()
        {
            transform.LookAt(_target.transform.position);
            _actionScheduler.StartAction(this);

            // Throttles the frequency of attacks
            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        /*
         * This makes sure that the animator triggers are in the
         * correct state for an attack.  If stopAttack is still
         * triggered, it will cause a partial animation and then
         * reset itself.
        */
        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }

        /*
         * This makes sure that the animator triggers are in the 
         * correct state for an attack.
        */
        private void StopAttack()
        {
            _animator.SetTrigger("stopAttack");
            _animator.ResetTrigger("attack");
        }

        /*
         * Animation event - called by animation
         * This is the point in the animation where the character
         * actually hits the target.  This can be used to start particle
         * effects, sound effects, etc
        */
        public void Hit()
        {   
            if (_target == null)
            {
                return;
            }

            _target.TakeDamage(_weaponDamage);
        }
    }
}
