using RPG.Combat;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float _chaseDistance;

        private GameObject _player; 
        private Mover _mover;
        private Fighter _fighter;


        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            if (_player == null )
            {
                Debug.LogError("Player is Null!");
            }

            _mover = GetComponent<Mover>();
            if (_mover == null)
            {
                Debug.LogError("Mover is Null!");
            }

            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
            {
                Debug.LogError("Fighter is Null!");
            }
        }

        void Update()
        {
            if(GetDistanceToPlayer() < _chaseDistance)
            {
                Debug.Log(this.gameObject.name + " is chasing!");
            }
        }

        private float GetDistanceToPlayer()
        {
            if (_player != null)
            {
                return Vector3.Distance(this.transform.position, _player.transform.position);
            }
            return 1000;
        }
    }
}
