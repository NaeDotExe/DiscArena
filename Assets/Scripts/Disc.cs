using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disc : MonoBehaviour
{
    #region Attributes
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _minVelocity = 1.0f;

    protected bool _isThrown = false;
    protected LineRenderer _lineRenderer = null;
    protected Rigidbody _rigidbody = null;
    #endregion

    #region Properties
    public bool IsThrown
    {
        get { return _isThrown; }
    }
    public Vector3 Velocity
    {
        get { return _rigidbody.velocity; }
    }
    #endregion

    #region Events
    //public UnityEvent OnDestroyed = new UnityEvent();
    #endregion

    #region Methods
    private void Update()
    {
        if (_isThrown)
        {
            if (Velocity.x < 2.5f && Velocity.x > -2.5f)
            {
                if (Velocity.z < 2.5f && Velocity.z > -2.5f)
                {
                    // Use this instead
                    //OnDestroyed.Invoke();

                    FindObjectOfType<DiscShooter>().OnDiscDestroyed();
                    Destroy(gameObject);
                }
            }

            //Debug.LogFormat("X : {0} || Y : {1}", Velocity.x, Velocity.z);
        }
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("No Component Rigidbody found!");
            return;
        }

        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            Debug.LogError("No Component LineRenderer found!");
            return;
        }
    }

    public void AddForce(Vector3 direction, float multiplier)
    {
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody is null!");
            return;
        }

        _rigidbody.AddForce(direction * multiplier, ForceMode.Impulse);

        _lineRenderer.enabled = true;

        StartCoroutine(IsThrownCoroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();
        if (destroyable == null)
        {
            return;
        }

        destroyable.TakeDamage(_damage);

        // Use event instead
        FindObjectOfType<DiscShooter>().OnDiscHitObstacle();
    }
    
    private IEnumerator IsThrownCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        _isThrown = true;
    }
    #endregion
}
