using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscShooter : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _shootMultiplier = 10f;
    [SerializeField] private Vector3 _discStartPosition = Vector3.zero;
    [SerializeField] private TrajectoryDrawer _trajectoryDrawer = null;

    private int _discCount = 5;
    [SerializeField] private Disc _currentDisc = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _currentPos = Vector3.zero;

    private Vector2 direction = Vector3.zero;
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
    private void Start()
    {
        direction = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _currentPos = Input.mousePosition;
            direction = (_currentPos - _startPos).normalized; 
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
    private void ShootDisc(Vector2 direction)
    {
        Vector3 convertedDirection = new Vector3(-direction.x, 0f, -direction.y);

        _currentDisc.IsThrown = true;
        _currentDisc.AddForce(convertedDirection, _shootMultiplier);
    }

    private void OnDrawGizmos()
    {
        return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_discStartPosition, 0.5f);
    }
    #endregion
}
