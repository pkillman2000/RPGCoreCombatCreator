using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Vector3 _globalVelocity;
    private Vector3 _localVelocity;


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null!");
        }

        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Animator is Null!");
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit) 
        {
            _navMeshAgent.destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        _globalVelocity = _navMeshAgent.velocity;
        // Converts from global to local velocity
        _localVelocity = transform.InverseTransformDirection(_globalVelocity);
        // Only interested in forward axis (Z)
        _animator.SetFloat("forwardSpeed", _localVelocity.z);
    }
}
