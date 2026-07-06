using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Recall")]
public class Recall : HeroAbility
{
    [SerializeField] private RecallData data;

    public override void Activate(PlayerManager player)
    {
        HistorySystem recorder =
            player.GetComponent<HistorySystem>();

        if (recorder == null)
            return;

        recorder.Restore(player);
    }

    public override AbilityData GetData()
    {
        return data;
    }
}