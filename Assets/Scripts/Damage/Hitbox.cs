using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private bool critical;

    public float DamageMultiplier => damageMultiplier;
    public bool Critical => critical;
}