using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    #region Attributes
    [SerializeField] private SpriteRenderer _bar = null;

    private Vector3 scale = Vector3.one;
    #endregion

    #region Methods
    private void Start()
    {
        scale = _bar.transform.localScale;
    }

    public void SetValue(float value)
    {
        scale.x = value;
        _bar.transform.localScale = scale;
    }
    #endregion
}
