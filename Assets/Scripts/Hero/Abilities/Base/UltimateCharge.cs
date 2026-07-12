using UnityEngine;

public class UltimateCharge : MonoBehaviour
{
    [SerializeField] private float maxCharge = 100f;

    public float CurrentCharge { get; private set; }

    public float MaxCharge => maxCharge;

    public bool IsReady => CurrentCharge >= maxCharge;

    public void Add(float amount)
    {
        CurrentCharge = Mathf.Clamp(CurrentCharge + amount, 0f, maxCharge);
    }

    public bool TryConsume()
    {
        if (!IsReady)
            return false;

        CurrentCharge = 0f;
        return true;
    }
}