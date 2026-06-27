using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 20f;

    public override void Shoot()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, range))
            return;

        Debug.Log($"Hit: {hit.collider.name}");

        IDamageable damageable = hit.collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}