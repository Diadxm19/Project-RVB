using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // Start is called before the first frame update
    private enum gunType { SHOTGUN, AR, SMG, LMG, SNIPER, DMR };
    private gunType GunType = gunType.SHOTGUN;
    private enum fireType { AUTO, BURST, SEMI, SINGLE };
    private fireType FireType;
    private int Ammo;
    private int Reserve;
    private float Reload;
    private float WeaponAccuracy;
    private float RateOfFire;

    public void WeaponStart(int guntype, int ammo, int reserve, float reload, float weaponaccuracy, float rateoffire)
    {
        SetGunType(guntype);
        Ammo = ammo;
        Reserve = reserve;
        Reload = reload;
        WeaponAccuracy = weaponaccuracy;
        RateOfFire = rateoffire;
    }

    private void SetGunType(int i)
    {
        switch (i)
        {
            case 0:
                GunType = gunType.SHOTGUN;
                break;

            case 1:
                GunType = gunType.AR;
                break;

            case 2:
                GunType = gunType.SMG;
                break;

            case 3:
                GunType = gunType.LMG;
                break;

            case 4:
                GunType = gunType.SNIPER;
                break;

            case 5:
                GunType = gunType.DMR;
                break;
        }
    }

    public void ShotBullet()
    {
        SetAmmoCount(GetAmmo() - 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /////////////////////////////////////////////////////////////////
    //Setters//
    public void SetAmmoCount(int i) { Ammo = i; }
    public void SetReserveCount(int i) { Reserve = i; }
    public void SetReloadSpeed(float f) { Reload = f; }
    public void SetWeaponAccuracy(float f) { WeaponAccuracy = f; }
    public void SetRateOfFire(float r) { RateOfFire = r; }

    //Getters//
    public int GetAmmo() { return Ammo; }
    public int GetReserve() { return Reserve; }
    public float GetReload() { return Reload; }
    public float GetWeaponAccuracy() { return WeaponAccuracy; }
    public float GetRateOfFire() { return RateOfFire; }
}
