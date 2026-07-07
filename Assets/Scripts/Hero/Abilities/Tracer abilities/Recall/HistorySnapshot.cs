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

    public void Apply(PlayerManager player)
    {
        CharacterController controller = player.Controller;

        controller.enabled = false;

        player.transform.position = Position;
        player.transform.rotation = Rotation;

        controller.enabled = true;
    }
}