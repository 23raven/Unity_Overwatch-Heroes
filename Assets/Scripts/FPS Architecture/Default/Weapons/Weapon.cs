using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected PlayerManager playerManager;
    [SerializeField] protected float fireRate = 10f;
    [SerializeField] protected FireMode fireMode;
    [SerializeField] protected int magazineSize = 30;
    [SerializeField] protected float reloadTime = 1.5f;

    protected int currentAmmo;
    protected bool isReloading;
    public bool IsReloading => isReloading;
    private float nextFireTime;

    public int CurrentAmmo => currentAmmo;
    public int MagazineSize => magazineSize;

    public virtual void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        currentAmmo = magazineSize;
    }

    public abstract void Shoot();

    protected bool CanShoot()
    {
        if (isReloading)
            return false;

        if (Time.time < nextFireTime)
            return false;

        nextFireTime = Time.time + 1f / fireRate;
        return true;
    }

    public bool WantsToShoot(PlayerInput input)
    {
        return fireMode switch
        {
            FireMode.SemiAuto => input.FirePressed,
            FireMode.FullAuto => input.FireHeld,
            _ => false
        };
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    protected void ConsumeAmmo()
    {
        currentAmmo--;
        Debug.Log($"{CurrentAmmo}/{MagazineSize}");
    }

    public void Reload()
    {
        if (isReloading)
            return;

        if (currentAmmo == magazineSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;

        Debug.Log($"Reload complete: {CurrentAmmo}/{MagazineSize}");
    }

}