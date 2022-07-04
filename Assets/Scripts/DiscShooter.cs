using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscShooter : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _shootMultiplier = 10f;
    [SerializeField] private Vector3 _discStartPosition = Vector3.zero;

    private int _discCount = 5;
    [SerializeField] private Disc _currentDisc = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _currentPos = Vector3.zero;
    private List<Disc> _pool = new List<Disc>();
    #endregion

    #region Properties
    public int DiscCount
    {
        get { return _discCount; }
        set { _discCount = value; }
    }
    public Disc CurrentDisc
    {
        get { return _currentDisc; }
    }
    #endregion

    #region Methods
    private void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)));

        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            /*Vector3*/
            _currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log(_currentPos);
            direction = -(_currentPos - _startPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShootDisc(direction);
        }
    }

    private void InstantiateNewDisc()
    {
        if (_discCount == 0)
        {
            return;
        }
    }
    private void ShootDisc(Vector3 direction)
    {
        _currentDisc.AddForce(Vector3.forward + Vector3.left * 0.8f, _shootMultiplier);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_discStartPosition, 0.5f);

        Gizmos.color = Color.red;

        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
        //Gizmos.DrawLine(_startPos, _currentPos);
        Gizmos.DrawSphere(new Vector3(-vec.x, 1, -(vec.y + 10)), 0.5f);
    }
    #endregion
}
