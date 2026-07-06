using UnityEngine;

public struct HistorySnapshot
{
    public Vector3 Position;
    public Quaternion Rotation;
    public float Health;

    public HistorySnapshot(
        Vector3 position,
        Quaternion rotation,
        float health)
    {
        Position = position;
        Rotation = rotation;
        Health = health;
    }
}