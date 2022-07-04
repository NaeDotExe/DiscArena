using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : Destroyable
{
    #region Attributes
    [SerializeField] private float _horizontalMovement = 5f;

    private int _currentPosition = 0;
    private bool _allowMovement = false;
    private bool _moveRight = false;
    private List<Vector3> _positions = new List<Vector3>();
    #endregion

    #region Methods
    protected override void Start()
    {
        Vector3 min = new Vector3(transform.position.x - _horizontalMovement, transform.position.y, transform.position.z);
        Vector3 max = new Vector3(transform.position.x + _horizontalMovement, transform.position.y, transform.position.z);

        _positions.Add(min);
        _positions.Add(transform.position);
        _positions.Add(max);

        _currentPosition = 1;
        transform.position = _positions[_currentPosition];
    }
    private void Update()
    {
        if (_allowMovement)
        {
            if(_moveRight)
            {

            }
        }
    }
    #endregion
}
