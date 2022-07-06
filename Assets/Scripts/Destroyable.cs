using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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

    protected int _currentHp = 0;
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

    #region Events
    [HideInInspector]
    public UnityEvent OnDestroyed = new UnityEvent();
    #endregion

    #region Methods
    protected virtual void Start()
    {
        _currentHp = _maxHp;
        _lifeBar.SetValue(((float)_currentHp / (float)_maxHp));
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        // Feedbacks
        _lifeBar.SetValue(((float)_currentHp / (float)_maxHp));
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
    }

    protected virtual void Destroy()
    {
        OnDestroyed.Invoke();

        Destroy(gameObject);
    }
    #endregion
}
