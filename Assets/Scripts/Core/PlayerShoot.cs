using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerManager playerManager;
    private WeaponHolder weaponHolder;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        weaponHolder = manager.WeaponHolder;

        weaponHolder.CurrentWeapon.Shot += OnWeaponShot;
    }

    private void OnWeaponShot()
    {
        playerManager.AudioManager.PlayShoot();
    }

    private void Update()
    {
        HandleShoot();
        HandleReload();
    }

    private void HandleShoot()
    {
        bool wantsToShoot = weaponHolder.CurrentWeapon.WantsToShoot(playerManager.Input);

        HandleShootAudio(wantsToShoot);

        if (!wantsToShoot)
            return;

        weaponHolder.CurrentWeapon.Shoot();
    }

    private void HandleReload()
    {
        if (!playerManager.Input.ReloadPressed)
            return;

        weaponHolder.CurrentWeapon.Reload();
    }

    private void HandleShootAudio(bool wantsToShoot)
    {
        if (!wantsToShoot)
            playerManager.AudioManager.StopShoot();
    }
}