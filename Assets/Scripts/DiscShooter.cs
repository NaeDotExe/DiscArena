using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscShooter : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _shootForceMultiplier = 25f;
    [SerializeField] private Vector3 _discStartPosition = Vector3.zero;
    [SerializeField] private TrajectoryDrawer _trajectoryDrawer = null;
    [SerializeField] private TouchZone _touchZone = null;
    [SerializeField] private Disc _discPrefab = null;

    private int _discCount = 5;
    private bool _isPointerDown = false;
    private Disc _currentDisc = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _currentPos = Vector3.zero;
    private Vector2 direction = Vector3.zero;
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

        _touchZone.OnPointerClick.AddListener(OnPointerClick);
        _touchZone.OnPointerDown.AddListener(OnPointerDown);
        _touchZone.OnPointerUp.AddListener(OnPointerUp);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isPointerDown)
        {
            _startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && _isPointerDown)
        {
            _currentPos = Input.mousePosition;
            direction = (_currentPos - _startPos).normalized;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (direction.x != 0 && direction.y != 0)
            {
                ShootDisc(direction);
            }
        }
    }

    private void InstantiateNewDisc()
    {
        if (_discCount == 0)
        {
            return;
        }

        Disc disc = Instantiate(_discPrefab, _discStartPosition, Quaternion.identity);
        if (disc == null)
        {
            Debug.LogError("Failed to instantiate Disc!");
            return;
        }

        disc.Init();

        _currentDisc = disc;
    }
    private void ShootDisc(Vector2 direction)
    {
        if (_currentDisc == null)
        {
            return;
        }

        Vector3 convertedDirection = new Vector3(-direction.x, 0f, -direction.y);

        _currentDisc.AddForce(convertedDirection, _shootForceMultiplier);

        _currentDisc = null;
        _startPos = Vector3.zero;
        _currentPos = Input.mousePosition;
        direction = Vector3.zero;
    }

    private void OnPointerDown()
    {
        _isPointerDown = true;
    }
    private void OnPointerUp()
    {
        _isPointerDown = false;
    }
    private void OnPointerClick()
    {
        if (_currentDisc != null)
        {
            return;
        }
        
        InstantiateNewDisc();
    }

    private void OnDrawGizmos()
    {
        return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_discStartPosition, 0.5f);
    }
    #endregion
}
