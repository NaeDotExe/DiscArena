using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Attributes
    private int _discCount = 5;
    private Disc _currentDisc = null;
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
        
    }
    private void Update()
    {
        
    }

    private void InstantiateNewDisc()
    {
        if (_discCount == 0)
        {
            return;
        }

    }
    private void OnFingerDown()
    {

    }
    private void ShootDisc(Vector3 angle)
    {

    }
    #endregion
}
