using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Attributes
    [SerializeField] private DiscShooter _discShooter = null;
    [SerializeField] private List<Chest> _chests = new List<Chest>();

    [Space]
    [SerializeField] private VictoryDefeatPanel _victoryDefeatPanel = null;
    [SerializeField] private Animator _camAnimator = null;

    private int _destroyedCount = 0;
    #endregion

    #region Events
    public UnityEvent OnVictory = new UnityEvent();
    public UnityEvent OnDefeat = new UnityEvent();
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

        _discShooter.OnInstantiationDisabled.AddListener(Defeat);
    }

    private void OnChestDestroyed(Chest chest)
    {
        ++_destroyedCount;
        if (_destroyedCount >= _chests.Count)
        {
            chest.ShowHealthBar(false);

            Victory(chest);
        }
    }
    public void Victory(Chest chest)
    {
        StartCoroutine(VictoryCoroutine(chest));
    }
    public void Defeat()
    {
        OnDefeat.Invoke();

        _victoryDefeatPanel.OnDefeat();
    }
    private IEnumerator VictoryCoroutine(Chest chest)
    {
        _camAnimator.ResetTrigger("Victory");
        _camAnimator.SetTrigger("Victory");

        yield return new WaitForSeconds(0.5f);

        chest.SpawnCoins();

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1f;
        OnVictory.Invoke();

        _victoryDefeatPanel.OnVictory();
    }
    #endregion
}
