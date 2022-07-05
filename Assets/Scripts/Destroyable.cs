using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Destroyable : MonoBehaviour
{
    #region Attributes
    [SerializeField] protected int _maxHp = 3;

    [Space]
    [SerializeField] protected int _hpForParticlesActivation = 1;

    [Space]
    [SerializeField] protected HealthBar _lifeBar = null;
    //[SerializeField] protected TextMeshProUGUI _damageText = null;
    [SerializeField] protected ParticleSystem _particleSystem = null;

    [Space]
    [SerializeField] protected float _invicibilityDuration = 0.1f;

    protected int _currentHp = 0;
    //protected bool _isInvicible = false;
    #endregion

    #region Properties
    public int CurrentHp
    {
        get { return _currentHp; }
    }
    public int MaxHp
    {
        get { return _maxHp; }
    }
    #endregion

    #region Methods
    protected virtual void Start()
    {
        _currentHp = _maxHp;
        _lifeBar.SetValue((float)(_currentHp / _maxHp));
        //_isInvicible = false;
    }

    public void TakeDamage(int damage)
    {
        //if (_isInvicible)
        //    return;

        Debug.Log(gameObject);

        //_isInvicible = true;

        _currentHp -= damage;

        Debug.LogFormat("{0} :  hp = {1}, damaeg = {2}", gameObject, _currentHp, damage);

        // Feedbacks
        _lifeBar.SetValue((float)((float)_currentHp / (float)_maxHp));
        //_damageText.text = damage.ToString();

        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Destroy();
        }
        else if (_currentHp <= _hpForParticlesActivation && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }

        //StartCoroutine(ResetInvicibilityCoroutine());
    }

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }

    //protected IEnumerator ResetInvicibilityCoroutine()
    //{
    //    yield return new WaitForSeconds(_invicibilityDuration);

    //    _isInvicible = false;
    //}
    #endregion
}
