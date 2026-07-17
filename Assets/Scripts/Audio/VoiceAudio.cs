using UnityEngine;

/// <summary>
/// Handles voice lines.
/// </summary>
public class VoiceAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioPlayer audioPlayer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] voiceClips;

    /// <summary>
    /// Plays a random voice line.
    /// </summary>
    public void PlayRandomVoice()
    {
        audioPlayer.PlayRandom(voiceClips);
    }
}