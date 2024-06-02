using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    void Start()
    {
        if(_target == null)
        {
            Debug.LogError("Target is Null!");
        }
    }

    void Update()
    {
        this.transform.position = _target.position;
    }
}
