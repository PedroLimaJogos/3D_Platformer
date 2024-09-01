using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;
    public float startLife = 10f;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public UIFillUpdate UIFillUpdate;


    private void Awake() {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }
    public void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject,3f);
        
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if(_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(UIFillUpdate!=null)
        {
            UIFillUpdate.UpdateValue((float)_currentLife/startLife);
        }
    }
}
