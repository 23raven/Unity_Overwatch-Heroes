using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerManager playerManager;
    private WeaponHolder weaponHolder;

    public void Initialize(PlayerManager manager)
    {
        Debug.Log("PlayerShoot.Initialize");
        playerManager = manager;
        weaponHolder = manager.WeaponHolder;
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!playerManager.Input.FirePressed)
            return;

        weaponHolder.CurrentWeapon.Shoot();
    }
}