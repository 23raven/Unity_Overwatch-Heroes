using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected PlayerManager playerManager;
    [SerializeField] protected float fireRate = 10f;

    private float nextFireTime;

    public virtual void Initialize(PlayerManager manager)
    {
        playerManager = manager;
    }

    public abstract void Shoot();

    protected bool CanShoot()
    {
        if (Time.time < nextFireTime)
            return false;

        nextFireTime = Time.time + 1f / fireRate;
        return true;
    }
}