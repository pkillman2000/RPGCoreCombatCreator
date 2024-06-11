using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RPG.Core
{
    /*
     * This is a temporary script that will eventually
     * be replaced by Cinemachine
     */
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        void Start()
        {
            if (_target == null)
            {
                Debug.LogError("Target is Null!");
            }
        }
        // Wait for animation to update before moving camera
        void LateUpdate()
        {
            this.transform.position = _target.position;
        }
    }
}
