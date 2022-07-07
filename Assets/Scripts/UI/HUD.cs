using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    #region Attributes
    [SerializeField] private DiscShooter _discShooter = null;

    [Space]
    [SerializeField] private Animator _discTextAnimator = null;
    [SerializeField] private TextMeshProUGUI _discCountText = null;
    [SerializeField] private string _discCountFormat = "{0} DISCS REMAINING";
    #endregion

    private void Start()
    {
        _discShooter.OnDiscCountUpdated.AddListener(OnDiscCountUpdated);
    }

    private void OnDiscCountUpdated(int count)
    {
        _discCountText.text = string.Format(_discCountFormat, count);
        _discTextAnimator.ResetTrigger("CountUpdated");
        _discTextAnimator.SetTrigger("CountUpdated");
    }
}
