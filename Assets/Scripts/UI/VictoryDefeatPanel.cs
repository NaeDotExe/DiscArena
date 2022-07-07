using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDefeatPanel : MonoBehaviour
{
    #region Attributes
    [SerializeField] private Button _retry = null;
    [SerializeField] private Button _quit = null;

    private Animator _animator = null;
    #endregion

    #region Methods
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is null.");
            return;
        }

        _retry.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(0));
        _quit.onClick.AddListener(Application.Quit);
    }

    public void OnVictory()
    {
        _animator.ResetTrigger("Victory");
        _animator.SetTrigger("Victory");
    }
    public void OnDefeat()
    {
        _animator.ResetTrigger("Defeat");
        _animator.SetTrigger("Defeat");
    }
    #endregion
}
