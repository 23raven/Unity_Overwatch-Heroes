using System.Collections.Generic;
using UnityEngine;

public class HistorySystem : MonoBehaviour
{
    private const float RecordInterval = 0.05f;
    private const float MaxRecordTime = 3f;

    private readonly List<HistorySnapshot> history = new();

    private float timer;

    private PlayerManager player;

    public IReadOnlyList<HistorySnapshot> History => history;



    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < RecordInterval)
            return;

        timer = 0f;

        Record();
    }

    private void Record()
    {
        history.Add(new HistorySnapshot(
            transform.position,
            transform.rotation,
            100f)); // Пока временно

        int maxSnapshots = Mathf.CeilToInt(MaxRecordTime / RecordInterval);

        if (history.Count > maxSnapshots)
        {
            history.RemoveAt(0);
        }
    }

    public HistorySnapshot GetOldestSnapshot()
    {
        if (history.Count == 0)
            return default;

        return history[0];
    }

    public void Restore(PlayerManager player)
    {
        if (history.Count == 0)
            return;

        HistorySnapshot snapshot = history[0];

        CharacterController controller = player.Controller;

        controller.enabled = false;

        player.transform.position = snapshot.Position;
        player.transform.rotation = snapshot.Rotation;

        controller.enabled = true;
    }


}