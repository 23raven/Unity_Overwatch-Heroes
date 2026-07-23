using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Blink Data")]
public class BlinkData : AbilityData
{
    [SerializeField] private float distance = 7f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private float effectDelay = 0.02f;

    public float Distance => distance;
    public float Cooldown => cooldown;
    public float EffectDelay => effectDelay;

}