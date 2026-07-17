using UnityEngine;

public class ShootingBot : TrainingBot
{
    [Header("Combat")]
    [SerializeField] private Transform target;
    [SerializeField] private Weapon weapon;

    [Header("Settings")]
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float rotationSpeed = 8f;

    private void Update()
    {
        if (target == null)
            return;

        if (!IsTargetInRange())
            return;

        RotateTowardsTarget();

        weapon.Shoot();
    }

    private bool IsTargetInRange()
    {
        float distance = Vector3.Distance(
            transform.position,
            target.position);

        return distance <= attackRange;
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}