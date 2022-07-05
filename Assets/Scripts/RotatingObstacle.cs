using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : Destroyable
{
    #region Attributes
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _rotationDelay = 0.5f;

    [Space]
    [SerializeField] private Vector3 _minEuler = Vector3.zero;
    [SerializeField] private Vector3 _maxEuler = Vector3.zero;

    private bool _allowUpdate = true;
    private bool _rotateRight = false;
    #endregion

    #region Methods
    private void Update()
    {
        return;

        if (_allowUpdate)
        {
            if (_rotateRight)
            {
                transform.eulerAngles = Vector3.Lerp(_minEuler, _maxEuler, Time.deltaTime * _rotationSpeed);
                if (transform.eulerAngles == _maxEuler)
                {
                    _allowUpdate = false;
                    StartCoroutine(ChangeRotation(_rotationDelay));
                }
            }
            else
            {
                transform.eulerAngles = Vector3.Lerp(_maxEuler, _minEuler, Time.deltaTime * _rotationSpeed);
                if (transform.eulerAngles == _minEuler)
                {
                    _allowUpdate = false;
                    StartCoroutine(ChangeRotation(_rotationDelay));
                }
            }
        }
    }

    private IEnumerator ChangeRotation(float delay)
    {
        yield return new WaitForSeconds(delay);

        _allowUpdate = true;
        _rotateRight = !_rotateRight;
    }
    #endregion
}
