using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Recall Data")]
public class RecallData : AbilityData
{
    [Header("Recall")]
    public float RecallDuration = 1.25f;

    public bool RestoreHealth = true;
}