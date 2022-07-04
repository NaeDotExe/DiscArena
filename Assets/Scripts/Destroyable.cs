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
    [SerializeField] protected Slider _lifeBar = null;
    [SerializeField] protected TextMeshProUGUI _damageText = null;
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

    #region Methods
    protected virtual void Start()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        
        // Feedbacks
        _lifeBar.value = _currentHp / _maxHp;
        _damageText.text = damage.ToString();

        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Destroy();
        }
        else if (_maxHp == _hpForParticlesActivation && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
    #endregion
}
