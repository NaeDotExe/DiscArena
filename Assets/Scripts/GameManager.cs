using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Attributes
    [SerializeField] private List<Chest> _chests = new List<Chest>();

    private int _destroyedCount = 0;
    #endregion

    #region Events
    public UnityEvent OnLevelComplete = new UnityEvent();
    #endregion

    #region Methods
    private void Start()
    {
        BindEvents();
    }

    private void BindEvents()
    {
        foreach (Chest chest in _chests)
        {
            chest.OnDestroyed.AddListener(() => OnChestDestroyed(chest));
        }
    }

    private void OnChestDestroyed(Chest chest)
    {
        ++_destroyedCount;
        if (_destroyedCount >= _chests.Count)
        {
            LevelEnd(chest);
        }
    }
    private void LevelEnd(Chest chest)
    {
        StartCoroutine(LevelEndCoroutine(chest));
    }
    private IEnumerator LevelEndCoroutine(Chest chest)
    {
        chest.SpawnCoins();

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1f;
        OnLevelComplete.Invoke();
    }
    #endregion
}
