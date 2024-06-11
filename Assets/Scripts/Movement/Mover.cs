using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    /*
     * This contains character movement functions.
     * It will be used by the player and AI characters
     */
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private ActionScheduler _actionScheduler;

        private Vector3 _globalVelocity;
        private Vector3 _localVelocity;


        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_navMeshAgent == null)
            {
                Debug.LogError("Nav Mesh Agent is Null!");
            }

            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator is Null!");
            }

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
            {
                Debug.LogError("Action Scheduler is Null!");
            }
        }

        void Update()
        {
            UpdateAnimator();
        }

        // Cancels attack so character can move
        public void StartMoveAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        // Move to destination
        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        // Cancel moving
        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        // Update animation with current velocity
        private void UpdateAnimator()
        {
            _globalVelocity = _navMeshAgent.velocity;
            // Converts from global to local velocity
            _localVelocity = transform.InverseTransformDirection(_globalVelocity);
            // Only interested in forward axis (Z)
            _animator.SetFloat("forwardSpeed", _localVelocity.z);
        }
    }
}
