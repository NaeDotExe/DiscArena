using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    #region Attributes
    [SerializeField] private GameObject _object = null;

    [Space]
    [SerializeField] private bool _startMoveRight = false;
    [SerializeField] private float _moveSpeed = 5.0f;

    [Space]
    [SerializeField] private float _minX = -2f;
    [SerializeField] private float _maxX = 2f;

    private int _currentPosition = 0;
    private bool _allowMovement = false;
    private bool _moveRight = true;
    #endregion

    #region Methods
    private void Start()
    {
        _allowMovement = true;
        _moveRight = _startMoveRight;
    }
    private void Update()
    {
        if (_allowMovement)
        {
            if (_moveRight)
            {
                Vector3 pos = _object.transform.position;
                if (pos.x < _maxX)
                {
                    pos.x += Time.deltaTime * _moveSpeed;
                    _object.transform.position = pos;
                    if (_object.transform.position.x >= _maxX)
                    {
                        _moveRight = false;
                    }
                }

            }
            else
            {
                Vector3 pos = _object.transform.position;
                if (pos.x > _minX)
                {
                    pos.x -= Time.deltaTime * _moveSpeed;
                    _object.transform.position = pos;
                    if (_object.transform.position.x <= _minX)
                    {
                        _moveRight = true;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 min = new Vector3(_minX, _object.transform.position.y, _object.transform.position.z);
        Vector3 max = new Vector3(_maxX, _object.transform.position.y, _object.transform.position.z);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(min, 0.2f);
        Gizmos.DrawSphere(max, 0.2f);
        Gizmos.DrawLine(min, max);
    }
    #endregion
}
