using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Destroyable
{
    #region Attributes
    [SerializeField] private GameObject _coinPrefab = null;
    [SerializeField] private int _coinsCount = 20;
    #endregion

    #region Methods
    protected override void Start()
    {
        base.Start();
    }
    protected override void Destroy()
    {
        OnDestroyed.Invoke();

        //base.Destroy();
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < _coinsCount; ++i)
        {
            Vector3 position = transform.position + (Random.insideUnitSphere);
            Instantiate(_coinPrefab, position, Quaternion.identity);
        }
    }
    #endregion
}
