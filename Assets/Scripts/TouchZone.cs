using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchZone : MonoBehaviour
{
    #region Attributes
    private bool _isPointerDown = false;
    #endregion

    #region Properties
    public bool IsPointerDown
    {
        get { return _isPointerDown; }
    }
    #endregion

    #region Events
    [HideInInspector]
    public UnityEvent OnPointerUp = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPointerDown = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPointerClick = new UnityEvent();
    #endregion

    #region Methods
    public void PointerDown()
    {
        Debug.Log("Pointer Down");

        OnPointerDown.Invoke();
        
        _isPointerDown = true;
    }
    public void PointerUp()
    {
        Debug.Log("Pointer Up");

        OnPointerUp.Invoke();

        _isPointerDown = false;
    }
    public void PointerClick()
    {
        Debug.Log("Pointer Click");

        OnPointerClick.Invoke();
    }
    #endregion
}
