using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    #region Attributes
    [SerializeField] protected int _damage = 1;
    #endregion

    #region Methods
    protected void Start()
    {

    }
    protected void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();
        if (destroyable == null)
        {
            return;
        }

        destroyable.TakeDamage(_damage);
    }
    #endregion
}
