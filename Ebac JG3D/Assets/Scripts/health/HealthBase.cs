using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;
    public float startLife = 10f;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public UIFillUpdate UIFillUpdate;

    public float damageMultiply = 1;


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
        UpdateUI();
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
        _currentLife -= f * damageMultiply;

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

    public void ChangeDamage(float damage, float duration)
    {
        StartCoroutine(ChangeDamageCoroutine(damageMultiply,duration));
    }

    IEnumerator ChangeDamageCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }
}
