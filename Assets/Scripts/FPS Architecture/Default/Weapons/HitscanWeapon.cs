using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem impactEffect;

    public override void Shoot()
    {
        if (!CanShoot())
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, range))
            return;

        PlayMuzzleFlash();
        PlayImpactEffect(hit);
        DealDamage(hit);
    }

    private void DealDamage(RaycastHit hit)
    {
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(damage);
    }

    private void PlayMuzzleFlash()
    {
        Debug.Log("Muzzle Flash");

        if (muzzleFlash == null)
        {
            Debug.LogError("MuzzleFlash is NULL");
            return;
        }

        muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlash.Play();
    }

    private void PlayImpactEffect(RaycastHit hit)
    {
        if (impactEffect == null)
            return;

        Instantiate(
            impactEffect,
            hit.point,
            Quaternion.LookRotation(hit.normal));
    }
}