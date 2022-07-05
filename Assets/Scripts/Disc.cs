using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    #region Attributes
    [SerializeField] protected int _damage = 1;

    protected LineRenderer _lineRenderer = null;
    protected Rigidbody _rigidbody = null;
    #endregion

    #region Properties
    public Vector3 Velocity
    {
        get { return _rigidbody.velocity; }
    }
    #endregion

    #region Methods
    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("No Component Rigidbody found!");
            return;
        }

        _lineRenderer = GetComponent<LineRenderer>();
        if(_lineRenderer == null)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();
        if (destroyable == null)
        {
            return;
        }

        Debug.LogFormat("Disc collided with {0}", destroyable.gameObject);

        destroyable.TakeDamage(_damage);
    }
    #endregion
}
