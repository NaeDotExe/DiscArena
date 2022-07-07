using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscShooter : MonoBehaviour
{
    #region Attributes
    [SerializeField] private float _shootForceMultiplier = 25f;
    [SerializeField] private Vector3 _discStartPosition = Vector3.zero;
    [SerializeField] private TrajectoryDrawer _trajectoryDrawer = null;
    [SerializeField] private TouchZone _touchZone = null;
    [SerializeField] private Disc _discPrefab = null;
    [SerializeField] private int _maxDiscCount = 5;

    [Space]
    [SerializeField] private CameraShake _cameraShake = null;
    [SerializeField] private float _shakeDuration = 0.15f;
    [SerializeField] private float _shakeMagnitude = 0.2f;

    private int _currentDiscCount = 0;
    private bool _canInstantiate = true;
    private bool _isPointerDown = false;
    private Disc _currentDisc = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _currentPos = Vector3.zero;
    private Vector2 direction = Vector3.zero;
    #endregion

    #region Properties
    public int CurrentDiscCount
    {
        get { return _currentDiscCount; }
        set { _currentDiscCount = value; }
    }
    public Disc CurrentDisc
    {
        get { return _currentDisc; }
    }
    #endregion

    #region Events
    public UnityEvent OnInstantiationDisabled = new UnityEvent();
    public UnityEvent<int> OnDiscCountUpdated = new UnityEvent<int>();
    #endregion

    #region Methods
    private void Start()
    {
        direction = Vector2.zero;

        _touchZone.OnPointerClick.AddListener(OnPointerClick);
        _touchZone.OnPointerDown.AddListener(OnPointerDown);
        _touchZone.OnPointerUp.AddListener(OnPointerUp);

        _currentDiscCount = 0;
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

            Vector3 convertedDirection = new Vector3(-direction.x, 0f, -direction.y);

            if (_currentDisc != null)
            {
                _trajectoryDrawer.DrawTrajectory(_currentDisc.gameObject, new Vector3(-direction.x, 0f, -direction.y) * _shootForceMultiplier, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (direction.x != 0 && direction.y != 0)
            {
                _trajectoryDrawer.Clear();

                ShootDisc(direction);
            }
        }
    }

    private void InstantiateNewDisc()
    {
        if (!_canInstantiate)
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

        ++_currentDiscCount;
        if (_currentDiscCount == _maxDiscCount)
        {
            _canInstantiate = false;
        }
        else
        {
            OnDiscCountUpdated.Invoke(_maxDiscCount - _currentDiscCount);
        }
    }
    private void ShootDisc(Vector2 direction)
    {
        if (_currentDisc == null)
        {
            return;
        }

        Vector3 convertedDirection = new Vector3(-direction.x, 0f, -direction.y);

        _currentDisc.AddForce(convertedDirection, _shootForceMultiplier, ForceMode.Impulse);

        _currentDisc = null;
        _startPos = Vector3.zero;
        _currentPos = Input.mousePosition;
        direction = Vector3.zero;
    }
    public void OnDiscDestroyed()
    {
        if (!_canInstantiate)
        {
            OnInstantiationDisabled.Invoke();
        }
    }
    public void OnDiscHitObstacle()
    {
        _cameraShake.Shake(_shakeDuration, _shakeMagnitude);

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
        //Vibration.Vibrate(10);
#endif
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
