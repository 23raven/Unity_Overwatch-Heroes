using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected PlayerManager playerManager;

    public virtual void Initialize(PlayerManager manager)
    {
        playerManager = manager;
    }

    public abstract void Shoot();
}