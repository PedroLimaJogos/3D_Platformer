using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public List<UIFillUpdate> uiGunUpdaters;
    public List<GunBase> gunList;

    public GunBase gunBase;
    public List<GunBase> listGuns;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun(gunBase);

        inputs.Gameplay.Shoot.performed += cts => Shoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun(GunBase gun)
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }

        _currentGun = Instantiate(gun, gunPosition);
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        // Check for key presses to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrocaArma(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrocaArma(1);
        }
    }

    private void Shoot()
    {
        _currentGun.StartShoot();
    }
    private void CancelShoot()
    {
        _currentGun.StopShoot();
    }

    private void TrocaArma(int index)
    {
        if (index >= 0 && index < gunList.Count)
        {
            CreateGun(gunList[index]);
            Debug.Log($"Switched to weapon {index}");
        }
    }
}
