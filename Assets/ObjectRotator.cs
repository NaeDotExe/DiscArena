using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private bool _invertRotation = false;

    private void Start()
    {
        if (_invertRotation)
        {
            _animator.SetBool("Invert", true);
        }
    }
}
