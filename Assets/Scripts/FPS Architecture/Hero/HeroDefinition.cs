using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Hero Definition")]
public class HeroDefinition : ScriptableObject
{
    [Header("Movement")]
    public MovementSettings Movement;

    [Header("Weapon")]
    public WeaponData Weapon;
}